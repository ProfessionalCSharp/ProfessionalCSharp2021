using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;

namespace WebAppWithAppInsights
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.ConfigureAppConfiguration((context, config) =>
                //{
                //    DefaultAzureCredential credentials = new();
                //    string connectionString = context.Configuration["AzureAppConfigurationConnection"];
                //    config.AddAzureAppConfiguration(options =>
                //    {
                //        options.Connect(new Uri(connectionString), credentials);
                //    });
                //    //string? vaultUri = Environment.GetEnvironmentVariable("VaultUri");
                //    //if (vaultUri is null) return;
                //    //var keyVaultEndpoint = new Uri(vaultUri);
                //    //config.AddAzureKeyVault(
                //    //    keyVaultEndpoint,
                //    //    new DefaultAzureCredential());
                //})
                //.ConfigureLogging((context, logging) =>
                //{
                //    string instrumentationKey = context.Configuration["AppInsightsConnection"];
                //    logging.AddApplicationInsights(instrumentationKey);
                //})
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
