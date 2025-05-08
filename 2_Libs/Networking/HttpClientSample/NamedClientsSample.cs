using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

class NamedClientSample(
    IOptions<HttpClientSamplesOptions> options,
    IHttpClientFactory httpClientFactory,
    ILogger<HttpClientSamples> logger)
{
    private readonly ILogger _logger = logger;
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("racersClient");
    private readonly string _url = options.Value.InvalidUrl ?? "localhost:5052";

    public async Task RunAsync()
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/api/racers");
            response.EnsureSuccessStatusCode();

            Console.WriteLine($"Response Status Code: {(int)response.StatusCode} {response.ReasonPhrase}");
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }
}
