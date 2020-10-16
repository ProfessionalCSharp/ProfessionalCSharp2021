using MetricsSample;
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
            if (!string.IsNullOrEmpty(uri))
            {
                try
                {
                    var stopWatch = MainRunnerSource.Log.RequestStart("RunAsync", uri);
                    using var response = await _httpClient.GetAsync(uri);
                    response.EnsureSuccessStatusCode();
                    MainRunnerSource.Log.RequestStop(stopWatch);
                }
                catch (Exception ex) when (ex is HttpRequestException || ex is InvalidOperationException)
                {
                    MainRunnerSource.Log.Error(ex);
                    
                }
            }

        }
    }
}

