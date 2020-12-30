using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Hosting;
using System;

namespace AzureAppConfigWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((context, config) =>
                    {
                        // configuration is already needed from within setting up config
                        var settings = config.Build(); 
                        config.AddAzureAppConfiguration(options =>
                        {
                            // DefaultAzureCredential credential = new(includeInteractiveCredentials: true);
                            VisualStudioCredential credential = new();
                            var endpoint = settings["AppConfigEndpoint"];                            
                            options.Connect(new Uri(endpoint), credential)
                                .Select(KeyFilter.Any, LabelFilter.Null)
                                .Select(KeyFilter.Any, context.HostingEnvironment.EnvironmentName)
                                .ConfigureRefresh(refresh =>
                                {
                                    refresh.Register("AppConfigurationSample:Settings:Sentinel",
                                        refreshAll: true)
                                    .SetCacheExpiration(TimeSpan.FromMinutes(5));
                                })
                                .ConfigureKeyVault(kv =>
                                {
                                    kv.SetCredential(credential);
                                });
                        });                  
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
