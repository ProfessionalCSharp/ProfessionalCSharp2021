using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System.Net;
using System.Net.Sockets;

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

    public async Task SendAndReceiveAsync(CancellationToken cancellationToken)
    {
        try
        {
            var addresses = await Dns.GetHostAddressesAsync(_hostname);
            IPAddress ipAddress = addresses.Where(address => address.AddressFamily == AddressFamily.InterNetwork).First();
            if (ipAddress is null)
            {
                _logger.LogWarning("no IPv4 address");
                return;
            }

            Socket clientSocket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            await clientSocket.ConnectAsync(ipAddress, _serverPort, cancellationToken);

            _logger.LogInformation("client connected to echo service");
            using NetworkStream stream = new(clientSocket, ownsSocket: true);

            Console.WriteLine("enter text that is streamed to the server and returned");

            // send the input to the network stream
            Stream consoleInput = Console.OpenStandardInput();
            Task sender = consoleInput.CopyToAsync(stream, cancellationToken);

            // receive the output from the network stream
            Stream consoleOutput = Console.OpenStandardOutput();
            Task receiver = stream.CopyToAsync(consoleOutput, cancellationToken);

            await Task.WhenAll(sender, receiver);
            _logger.LogInformation("sender and receiver completed");
        }
        catch (SocketException ex) when (ex.ErrorCode == 10061)
        {
            _logger.LogError("Is the server running?");
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
