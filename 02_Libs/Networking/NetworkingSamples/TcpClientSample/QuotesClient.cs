using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

class QuotesClientOptions
{
    public string? Hostname { get; set; }
    public int ServerPort { get; set; }
}

class QuotesClient
{
    private readonly string _hostname;
    private readonly int _serverPort;
    private readonly ILogger _logger;
    public QuotesClient(IOptions<QuotesClientOptions> options, ILogger<QuotesClient> logger)
    {
        _hostname = options.Value.Hostname ?? "localhost";
        _serverPort = options.Value.ServerPort;
        _logger = logger;
    }

    public async Task SendAndReceiveAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            Memory<byte> buffer = new byte[4096].AsMemory();
            string? line;
            bool repeat = true;
            while (repeat)
            {
                Console.WriteLine(@"Press enter to read a quote, ""bye"" to exit");
                line = Console.ReadLine();
                if (line?.Equals("bye", StringComparison.CurrentCultureIgnoreCase) == true)
                {
                    repeat = false;
                }
                else
                {
                    TcpClient client = new();
                    await client.ConnectAsync(_hostname, _serverPort, cancellationToken);
                    using var stream = client.GetStream(); 
                    int bytesRead = await stream.ReadAsync(buffer, cancellationToken);
                    string quote = Encoding.UTF8.GetString(buffer.Span[..bytesRead]);
                    buffer.Span[..bytesRead].Clear();
                    Console.WriteLine(quote);
                    Console.WriteLine();
                }
            };
        }
        catch (SocketException ex)
        {
            _logger.LogError(ex, ex.Message);
        }

        Console.WriteLine("so long, and thanks for all the fish");
    }
}
