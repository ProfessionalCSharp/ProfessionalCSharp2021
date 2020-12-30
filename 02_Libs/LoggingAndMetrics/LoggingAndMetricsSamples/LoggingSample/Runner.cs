using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LoggingSample
{
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
                Console.Write("Please enter a URI or enter to exit: ");
                string? url = Console.ReadLine();
                using var _ = _logger.BeginScope("RunAsync, url: {0}", url);
                if (string.IsNullOrEmpty(url))
                {
                    exit = true;
                }
                else
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
            } while (!exit);
        }
    }
}
