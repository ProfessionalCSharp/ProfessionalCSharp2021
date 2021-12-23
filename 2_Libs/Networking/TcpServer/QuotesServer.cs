using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System.Net;
using System.Net.Sockets;
using System.Text;

record QuotesServerOptions
{
    public string? QuotesFile { get; init; }
    public int Port { get; init; }
}

class QuotesServer
{
    private readonly int _port;
    private readonly ILogger _logger;
    private readonly string _quotesPath;
    private string[]? _quotes;
    private Random _random = new();

    public QuotesServer(IOptions<QuotesServerOptions> options, ILogger<QuotesServer> logger)
    {
        _port = options.Value.Port;
        _quotesPath = options.Value.QuotesFile ?? "quotes.txt";
        _logger = logger;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken = default)
        => _quotes = await File.ReadAllLinesAsync(_quotesPath, cancellationToken);

    public async Task RunServerAsync(CancellationToken cancellationToken = default)
    {
        TcpListener listener = new(IPAddress.Any, _port);
        _logger.LogInformation("Quotes listener started on port {0}", _port);
        listener.Start();

        while (true)
        {
            cancellationToken.ThrowIfCancellationRequested();
            using TcpClient client = await listener.AcceptTcpClientAsync();
            _logger.LogInformation("Client connected with address and port: {0}", client.Client.RemoteEndPoint);
            var _ = SendQuoteAsync(client, cancellationToken);
        }
    }

    private async Task SendQuoteAsync(TcpClient client, CancellationToken cancellationToken = default)
    {
        try
        {
            client.LingerState = new LingerOption(true, 10);
            client.NoDelay = true;

            using var stream = client.GetStream(); // returns a stream that owns the socket
            var quote = GetRandomQuote();
            var buffer = Encoding.UTF8.GetBytes(quote).AsMemory();
            await stream.WriteAsync(buffer, cancellationToken);
        }
        catch (IOException ex)
        {
            _logger.LogError(ex, ex.Message);
        }
        catch (SocketException ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }

    private string GetRandomQuote()
    {
        if (_quotes is null) throw new InvalidOperationException($"Invoke InitializeAsync before calling {nameof(GetRandomQuote)}");

        return _quotes[_random.Next(_quotes.Length)];
    }
}
