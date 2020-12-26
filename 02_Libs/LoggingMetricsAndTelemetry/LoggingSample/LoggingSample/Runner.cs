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
            bool exit = false;
            do
            {
                Console.Write("Please enter a URI or 'exit' to exit:");
                string url = Console.ReadLine() ?? throw new InvalidOperationException("null returned from Console.ReadLine");
                if (url.ToLower() != "exit")
                {
                    try
                    {
                        using var _ = _logger.BeginScope("RunAsync iteration, url: {0}", url);
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
}
