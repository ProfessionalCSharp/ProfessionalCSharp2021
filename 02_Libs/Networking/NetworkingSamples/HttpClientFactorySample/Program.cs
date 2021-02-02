using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;

namespace HttpClientFactorySample
{
    class Program
    {
        static void Main(string[] args)
        {
            void ConfigureHttpClient(HttpClient client)
            {

            }

            using var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddHttpClient("x", ConfigureHttpClient);
                })
                .Build();
        }
    }
}
