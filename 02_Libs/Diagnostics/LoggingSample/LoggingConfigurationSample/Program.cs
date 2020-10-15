using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace LoggingConfigurationSample
{
    class Program
    {
        private static string s_url = "https://csharp.christiannagel.com";

        static async Task Main(string[] args)
        {
            if (args.Length == 1)
            {
                s_url = args[0];
            }

            using var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddHttpClient<SampleController>(client =>
                    {
                        client.BaseAddress = new Uri("");
                    }).AddTypedClient<SampleController>();
                }).Build();

            var controller = host.Services.GetRequiredService<SampleController>();
            await controller.NetworkRequestSampleAsync(s_url);
            Console.WriteLine("Completed");
            Console.ReadLine();
        }
    }
}
