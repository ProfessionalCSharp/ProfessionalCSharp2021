using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LoggingConfigurationSample
{
    class SampleController
    {
        private readonly ILogger<SampleController> _logger;
        private readonly HttpClient _httpClient;
        public SampleController(HttpClient httpClient, ILogger<SampleController> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _logger.LogTrace("ILogger injected into {0}", nameof(SampleController));
        }

        public async Task NetworkRequestSampleAsync(string url)
        {
            try
            {
                _logger.LogInformation(LoggingEvents.Networking, "NetworkRequestSampleAsync started with url {0}", url);

                string result = await _httpClient.GetStringAsync(url);
                _logger.LogInformation(LoggingEvents.Networking, "NetworkRequestSampleAsync completed, received {0} characters", result.Length);
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.Networking, ex, "Error in NetworkRequestSampleAsync, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
            }
        }
    }
}
