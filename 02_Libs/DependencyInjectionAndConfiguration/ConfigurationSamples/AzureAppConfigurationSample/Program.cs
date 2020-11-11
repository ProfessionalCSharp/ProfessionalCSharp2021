using AzureAppConfigurationSample;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        if (context.HostingEnvironment.IsDevelopment())
        {
            var settings = config.Build();
            string connectionString = settings["AzureAppConfigurationConnection"];
            config.AddAzureAppConfiguration(options =>
            {
                options.Connect(connectionString)
                    .Select(KeyFilter.Any, LabelFilter.Null)
                    .Select(KeyFilter.Any, context.HostingEnvironment.EnvironmentName);
            });
        }
        else
        {
            config.AddAzureAppConfiguration(options =>
            {
                options.Connect("", credential)
                    .Select(KeyFilter.Any, LabelFilter.Null)
                    .Select(KeyFilter.Any, context.HostingEnvironment.EnvironmentName);
            });
        }
    }).ConfigureServices(services =>
    {
        services.AddFeatureManagement();
        services.AddTransient<AppConfigurationSample>();
    }).Build();

var service = host.Services.GetRequiredService<AppConfigurationSample>();
service.Sample1();
