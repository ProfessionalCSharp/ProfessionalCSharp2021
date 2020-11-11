using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                        if (context.HostingEnvironment.IsDevelopment())
                        {
                            var settings = config.Build();
                            // use the connection string from user secrets including the secrets
                            string connectionString = settings["AzureAppConfigurationConnection"];

                            // config.AddAzureAppConfiguration(connectionString);
                            config.AddAzureAppConfiguration(options =>
                                options.Connect(connectionString)
                                    .Select(KeyFilter.Any, LabelFilter.Null)
                                    .Select(KeyFilter.Any, context.HostingEnvironment.EnvironmentName)
                                    .UseFeatureFlags()
                                    .ConfigureRefresh(refresh =>
                                    {
                                        refresh.Register("AppConfigurationSample:Settings:Sentinel", refreshAll: true)
                                            .SetCacheExpiration(TimeSpan.FromMinutes(5));
                                    }));
                        }
                    });                    
                    webBuilder.UseStartup<Startup>();
                });
    }
}
