using Microsoft.Extensions.Logging;

namespace MetricsSample;

class NetworkService
{
    private readonly ILogger<NetworkService> _logger;
    private readonly HttpClient _httpClient;
    public NetworkService(
        HttpClient httpClient,
        ILogger<NetworkService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _logger.LogTrace("ILogger injected into {0}", nameof(NetworkService));
    }

    public async Task NetworkRequestSampleAsync(Uri requestUri)
    {
        var stopWatch = MetricsSampleSource.Log.RequestStart();
        try
        {
            _logger.LogInformation(LoggingEvents.Networking, "NetworkRequestSampleAsync started with uri {0}", requestUri.AbsoluteUri);

            string result = await _httpClient.GetStringAsync(requestUri);
            MetricsSampleSource.Log.RequestStop(stopWatch);
            Console.WriteLine($"{result[..50]}");
            _logger.LogInformation(LoggingEvents.Networking, "NetworkRequestSampleAsync completed, received {0} characters", result.Length);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(LoggingEvents.Networking, ex, "Error in NetworkRequestSampleAsync, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
            MetricsSampleSource.Log.Error();
        }
        finally
        {
            MetricsSampleSource.Log.RequestStop(stopWatch);
        }
    }
}
