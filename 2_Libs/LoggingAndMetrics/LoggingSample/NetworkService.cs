﻿using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace LoggingSample;

class NetworkService
{
    // activity is span
    // activity?.SetTag("foo", foo);
    // https://docs.microsoft.com/en-us/dotnet/core/diagnostics/distributed-tracing-collection-walkthroughs#collect-traces-using-opentelemetry
    private static readonly ActivitySource s_activitySource = new("LoggingSample.DistributedTracing");
    private readonly ILogger<NetworkService> _logger;
    private readonly HttpClient _httpClient;
    public NetworkService(
        HttpClient httpClient,
        ILogger<NetworkService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _logger.LogTrace("ILogger injected into {service}", nameof(NetworkService));
    }

    public async Task NetworkRequestSampleAsync(Uri requestUri)
    {
        try
        {
            using var activity = s_activitySource.StartActivity("NetworkRequest");
            _logger.LogInformation(LoggingEvents.Networking, "NetworkRequestSampleAsync started with uri {uri}", requestUri.AbsoluteUri);

            string result = await _httpClient.GetStringAsync(requestUri);

            Console.WriteLine($"{result[..50]}");
            _logger.LogInformation(LoggingEvents.Networking, "NetworkRequestSampleAsync completed, received {length} characters", result.Length);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(LoggingEvents.Networking, ex, "Error in NetworkRequestSampleAsync, error message: {error}, HResult: {hresult}", ex.Message, ex.HResult);
        }
    }
}
