using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

class FaultHandlingSample(
    IOptions<HttpClientSamplesOptions> options,
    HttpClient httpClient,
    ILogger<HttpClientSamples> logger)
{
    private readonly ILogger _logger = logger;
    private readonly HttpClient _httpClient = httpClient;
    private readonly string _url = options.Value.InvalidUrl ?? "localhost:5052";

    public async Task RunAsync()
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_url);
            response.EnsureSuccessStatusCode();

            Console.WriteLine($"Response Status Code: {(int)response.StatusCode} {response.ReasonPhrase}");
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error: {error}", ex.Message);
        }
    }
}
