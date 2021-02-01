using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

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

    public HttpClientSamples(IOptions<HttpClientSamplesOptions> options, HttpClient httpClient, ILogger<HttpClientSamples> logger)
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
            Console.WriteLine(responseBodyAsText);
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

    public async Task ThrowExceptionAsync()
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
            Utilities.ShowHeaders("Request Headers:", _httpClient.DefaultRequestHeaders);
            HttpResponseMessage response = await _httpClient.GetAsync(_invalidUrl);
            response.EnsureSuccessStatusCode();

            Utilities.ShowHeaders("Response Headers:", response.Headers);

            Console.WriteLine($"Response Status Code: {response.StatusCode} {response.ReasonPhrase}");
            string responseBodyAsText = await (response.Content?.ReadAsStringAsync() ?? Task.FromResult(string.Empty));
            Console.WriteLine($"Received payload of {responseBodyAsText.Length} characters");
            Console.WriteLine();
            Console.WriteLine(responseBodyAsText);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }

    public async Task AddHttpHeadersAsync()
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
            Utilities.ShowHeaders("Request Headers:", _httpClient.DefaultRequestHeaders);

            HttpResponseMessage response = await _httpClient.GetAsync("/");
            response.EnsureSuccessStatusCode();

            Utilities.ShowHeaders("Response Headers:", response.Headers);

            Console.WriteLine($"Response Status Code: {(int)response.StatusCode} {response.ReasonPhrase}");
            string responseBodyAsText = await (response.Content?.ReadAsStringAsync() ?? Task.FromResult(string.Empty));
            Console.WriteLine($"Received payload of {responseBodyAsText.Length} characters");
            Console.WriteLine();
            Console.WriteLine(responseBodyAsText);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
}
