using Microsoft.Extensions.Logging;

public class HttpClientFactorySamples(
    IHttpClientFactory httpClientFactory,
    ILogger<HttpClientFactorySamples> logger)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("cni");
    private readonly ILogger _logger = logger;
}
