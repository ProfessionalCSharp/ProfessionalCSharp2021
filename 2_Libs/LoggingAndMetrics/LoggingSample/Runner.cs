using Microsoft.Extensions.Logging;

namespace LoggingSample;

class Runner(NetworkService networkService, ILogger<Runner> logger)
{
    private readonly ILogger _logger = logger;

    public async Task RunAsync()
    {
        _logger.LogDebug("RunAsync started");
        bool exit = false;
        do
        {
            Console.Write("Please enter a URI or enter to exit: ");
            string? url = Console.ReadLine();
            using var _ = _logger.BeginScope("RunAsync, url: {url}", url);
            if (string.IsNullOrEmpty(url))
            {
                exit = true;
            }
            else
            {
                try
                {
                    Uri uri = new(url);
                    await networkService.NetworkRequestSampleAsync(uri);
                }
                catch (UriFormatException ex)
                {
                    _logger.LogError(ex, "Error {message}", ex.Message);
                }
            }
        } while (!exit);
    }
}
