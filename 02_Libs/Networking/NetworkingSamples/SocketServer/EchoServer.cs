using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Buffers;
using System.IO;
using System.IO.Pipelines;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

class EchoServiceOptions
{
    public int Port { get; set; }
    public int Timeout { get; set; }
}

class EchoServer
{
    private readonly int _port;
    private readonly ILogger _logger;
    private readonly int _timeout;
    public EchoServer(IOptions<EchoServiceOptions> options, ILogger<EchoServer> logger)
    {
        _port = options.Value.Port;
        _timeout = options.Value.Timeout;
        _logger = logger;
    }

    public async Task StartListenerAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            using Socket listener = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.ReceiveTimeout = _timeout;
            listener.SendTimeout = _timeout;

            listener.Bind(new IPEndPoint(IPAddress.Any, _port));
            listener.Listen(backlog: 15);
            _logger.LogTrace("EchoListener started on port {0}", _port);
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    break;
                }
                var socket = await listener.AcceptAsync();
                if (!socket.Connected)
                {
                    _logger.LogWarning("Client not connected after accept");
                    break;
                }

                _logger.LogInformation("client connected, local {0}, remote {1}", socket.LocalEndPoint, socket.RemoteEndPoint);

                Task _ = ProcessClientJobAsync(socket);
            }
        }
        catch (SocketException ex)
        {
            _logger.LogError(ex, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    private async Task ProcessClientJobAsync(Socket socket, CancellationToken cancellationToken = default)
    {
        try
        {
            using NetworkStream stream = new(socket, ownsSocket: true);

            PipeReader reader = PipeReader.Create(stream);
            PipeWriter writer = PipeWriter.Create(stream);

            bool completed = false;
            do
            {
                ReadResult result = await reader.ReadAsync(cancellationToken);

                if (result.Buffer.Length == 0)
                {
                    completed = true;
                    _logger.LogInformation("received empty buffer, client closed");
                }
                ReadOnlySequence<byte> buffer = result.Buffer;
                if (buffer.IsSingleSegment)
                {
                    string data = Encoding.UTF8.GetString(buffer.FirstSpan);
                    _logger.LogTrace("received data {0} from the client {1}", data, socket.RemoteEndPoint);

                    // send the data back
                    await writer.WriteAsync(buffer.First, cancellationToken);
                }
                else
                {
                    int segmentNumber = 0;
                    foreach (var item in buffer)
                    {
                        segmentNumber++;
                        string data = Encoding.UTF8.GetString(item.Span);
                        _logger.LogTrace("received data {0} from the client {1} in the {2}. segment", data, socket.RemoteEndPoint, segmentNumber);

                        // send the data back
                        await writer.WriteAsync(item, cancellationToken);
                    }
                }
                SequencePosition nextPosition = result.Buffer.GetPosition(result.Buffer.Length);
                reader.AdvanceTo(nextPosition);

            } while (!completed);
        }
        catch (SocketException ex)
        {
            _logger.LogError(ex, ex.Message);
        }
        catch (IOException ex) when ((ex.InnerException is SocketException socketException) && (socketException.ErrorCode is 10054))
        {
            _logger.LogInformation("client {0} closed the connection", socket.RemoteEndPoint);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ex.Message with client {0}", socket.RemoteEndPoint);
            throw;
        }
        _logger.LogTrace("Closed stream and client socket {0}", socket.RemoteEndPoint);
    }
}
