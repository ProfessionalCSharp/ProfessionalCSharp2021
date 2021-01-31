using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


class HttpClientSampleWithMessageHandler
{
    private readonly HttpClient _httpClient;
    public HttpClientSampleWithMessageHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
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

