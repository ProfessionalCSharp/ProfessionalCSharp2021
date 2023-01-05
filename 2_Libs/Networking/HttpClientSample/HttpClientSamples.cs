using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Net.Http.Headers;

public record HttpClientSamplesOptions
{
    public string? Url { get; init; }
    public string? InvalidUrl { get; init; }
}

public class HttpClientSamples
{
    private readonly ILogger _logger;
    private readonly HttpClient _httpClient;
    private readonly string _url;
    private readonly string _invalidUrl;

    public HttpClientSamples(
        IOptions<HttpClientSamplesOptions> options, 
        HttpClient httpClient, 
        ILogger<HttpClientSamples> logger)
    {
        _url = options.Value.Url ?? "https://localhost:5020";
        _invalidUrl = options.Value.InvalidUrl ?? "https://localhost1:5020";
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task SimpleGetRequestAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("/");
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Response Status Code: {(int)response.StatusCode} {response.ReasonPhrase}");
            string responseBodyAsText = await (response.Content?.ReadAsStringAsync() ?? Task.FromResult(string.Empty));
            Console.WriteLine($"Received payload of {responseBodyAsText.Length} characters");
            Console.WriteLine();
            Console.WriteLine(responseBodyAsText[..50]);
        }
    }

    public async Task ThrowExceptionAsync()
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_invalidUrl);
            response.EnsureSuccessStatusCode();

            Console.WriteLine($"Response Status Code: {(int)response.StatusCode} {response.ReasonPhrase}");
            string responseBodyAsText = await (response.Content?.ReadAsStringAsync() ?? Task.FromResult(string.Empty));
            Console.WriteLine($"Received payload of {responseBodyAsText.Length} characters");
            Console.WriteLine();
            Console.WriteLine(responseBodyAsText[..50]);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error {message}", ex.Message);
        }
    }

    public async Task UseHttpRequestMessageAsync()
    {
        HttpRequestMessage request = new(HttpMethod.Get, "/");
    
        HttpResponseMessage response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Response Status Code: {(int)response.StatusCode} {response.ReasonPhrase}");
            string responseBodyAsText = await (response.Content?.ReadAsStringAsync() ?? Task.FromResult(string.Empty));
            Console.WriteLine($"Received payload of {responseBodyAsText.Length} characters");
            Console.WriteLine();
            Console.WriteLine(responseBodyAsText);
        }
    }

    public async Task AddHttpHeadersAsync()
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            //_httpClient.DefaultRequestHeaders.Add("Accept", "application/xml");
            //_httpClient.DefaultRequestHeaders.Add("Accept", new[] { "application/xml", "*/*" });
            Utilities.ShowHeaders("Request Headers:", _httpClient.DefaultRequestHeaders);

            HttpResponseMessage response = await _httpClient.GetAsync("/");
            response.EnsureSuccessStatusCode();

            Utilities.ShowHeaders("Response Headers:", response.Headers);
            Console.WriteLine();
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error {message}", ex.Message);
        }
    }

    public async Task UseHttp2()
    {
        try
        {
            _logger.LogTrace("UseHttp2 started");
            HttpRequestMessage request1 = new(HttpMethod.Get, "/api/racersdelay");
            request1.Version = new Version("1.1");
            HttpRequestMessage request2 = new(HttpMethod.Get, "/api/racers");
            request2.Version = new Version("1.1");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Task<HttpResponseMessage> t1 = _httpClient.SendAsync(request1);
            Task<HttpResponseMessage> t2 = _httpClient.SendAsync(request2); ;
            await Task.WhenAll(t1, t2);
            stopwatch.Stop();
            _logger.LogTrace("UseHttp2 finished after {milliseconds} with status {code1} and {code2}", stopwatch.ElapsedMilliseconds, t1.Result.StatusCode, t2.Result.StatusCode);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
}
