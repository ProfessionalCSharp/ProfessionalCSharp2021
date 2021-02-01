using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;


class HttpClientSampleWithMessageHandler
{
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;
    public HttpClientSampleWithMessageHandler(
        HttpClient httpClient, 
        ILogger<HttpClientSampleWithMessageHandler> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task UseMessageHandlerAsync()
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

