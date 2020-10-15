using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class MainRunner
{
    private readonly HttpClient _httpClient;

    public MainRunner(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.WriteLine($"enter the URI to access or exit to exit");
            string? uri = Console.ReadLine();
            if (uri is "exit")
            {
                break;
            }
            using var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
        }
    }
}

