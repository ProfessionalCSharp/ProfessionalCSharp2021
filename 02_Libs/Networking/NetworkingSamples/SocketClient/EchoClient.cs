using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

class EchoClientOptions
{
    public string? Hostname { get; set; }
    public int ServerPort { get; set; }
}

class EchoClient
{
    private readonly string _hostname;
    private readonly int _serverPort;
    private readonly ILogger _logger;
    public EchoClient(IOptions<EchoClientOptions> options, ILogger<EchoClient> logger)
    {
        _hostname = options.Value.Hostname ?? "localhost";
        _serverPort = options.Value.ServerPort;
        _logger = logger;
    }

    public async Task SendAndReceiveAsync()
    {
        try
        {
            CancellationTokenSource cancellationTokenSource = new();

            var addresses = await Dns.GetHostAddressesAsync(_hostname);
            IPAddress ipAddress = addresses.Where(address => address.AddressFamily == AddressFamily.InterNetwork).First();
            if (ipAddress is null)
            {
                _logger.LogWarning("no IPv4 address");
                return;
            }
            using Socket clientSocket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            await clientSocket.ConnectAsync(ipAddress, _serverPort, cancellationTokenSource.Token);

            Console.CancelKeyPress += async (sender, e) =>
            {
                _logger.LogInformation("cancellation initiated by the user");
                clientSocket.Disconnect(reuseSocket: false);
                clientSocket.Close();
                cancellationTokenSource.Cancel();
            };

            _logger.LogInformation("client connected to echo service");
            NetworkStream stream = new(clientSocket);
            Console.WriteLine("enter text that is streamed to the server and returned");
            Stream consoleInput = Console.OpenStandardInput();

            // send the input to the network stream
            Task sender = consoleInput.CopyToAsync(stream, cancellationTokenSource.Token);

            // receive the output from the network stream
            Stream consoleOutput = Console.OpenStandardOutput();
            Task receiver = stream.CopyToAsync(consoleOutput, cancellationTokenSource.Token);

            await Task.WhenAll(sender, receiver);
            _logger.LogInformation("sender and receiver completed");
        }
        catch (SocketException ex)
        {
            _logger.LogError(ex, ex.Message);
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogInformation(ex.Message);
        }
    }
}
