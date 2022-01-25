using Microsoft.Extensions.Logging;

namespace MetricsSample;

class Runner
{
    private readonly ILogger _logger;
    private readonly NetworkService _networkSevice;
    public Runner(NetworkService networkService, ILogger<Runner> logger)
    {
        _networkSevice = networkService;
        _logger = logger;
    }

    public async Task RunAsync()
    {
        _logger.LogDebug("RunAsync started");
        bool exit = false;
        do
        {
            Console.Write("Please enter a URI or 'exit' to exit: ");
            string url = Console.ReadLine() ?? throw new InvalidOperationException("null returned from Console.ReadLine");
            using var _ = _logger.BeginScope("RunAsync iteration, url: {url}", url);
            if (url.ToLower() != "exit")
            {
                try
                {
                    Uri uri = new(url);
                    await _networkSevice.NetworkRequestSampleAsync(uri);
                }
                catch (UriFormatException ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
            }
            else
            {
                exit = true;
            }
        } while (!exit);
    }
}
