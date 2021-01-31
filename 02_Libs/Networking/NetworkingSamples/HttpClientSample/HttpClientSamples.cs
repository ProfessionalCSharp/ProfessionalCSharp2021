using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public class HttpClientSamples 
{
    private const string NorthwindUrl = "http://services.odata.org/Northwind/Northwind.svc/Regions";
    private const string IncorrectUrl = "http://services.odata.org/Northwind1/Northwind.svc/Regions";

    private HttpClient? _httpClient;
    public HttpClient HttpClient => _httpClient ??= new HttpClient();
    private HttpClient? _httpClientWithMessageHandler;
    public HttpClient HttpClientWithMessageHandler => _httpClientWithMessageHandler ??= new HttpClient(new SampleMessageHandler("error"));

    private ILogger _logger;
    public HttpClientSamples(ILogger<HttpClientSamples> logger)
    {
        _logger = logger;
    }

    public async Task SimpleGetRequestAsync()
    {
        HttpResponseMessage response = await HttpClient.GetAsync("/");
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
        HttpRequestMessage request = new(HttpMethod.Get, NorthwindUrl);

        HttpResponseMessage response = await HttpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Response Status Code: {(int)response.StatusCode} {response.ReasonPhrase}");
            string responseBodyAsText = await (response.Content?.ReadAsStringAsync() ?? Task.FromResult(string.Empty));
            Console.WriteLine($"Received payload of {responseBodyAsText.Length} characters");
            Console.WriteLine();
            Console.WriteLine(responseBodyAsText);
        }
    }

    public async Task GetDataWithExceptionsAsync()
    {
        try
        {
            HttpClient.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
            Utilities.ShowHeaders("Request Headers:", HttpClient.DefaultRequestHeaders);
            HttpResponseMessage response = await HttpClient.GetAsync(IncorrectUrl);
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
            HttpClient.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
            Utilities.ShowHeaders("Request Headers:", HttpClient.DefaultRequestHeaders);

            HttpResponseMessage response = await HttpClient.GetAsync(NorthwindUrl);
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
