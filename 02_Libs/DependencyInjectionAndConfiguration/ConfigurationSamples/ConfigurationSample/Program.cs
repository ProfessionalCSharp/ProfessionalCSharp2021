using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory());
        config.AddJsonFile("customconfigurationfile.json", optional: true);
    }).ConfigureServices(services =>
    {
        services.AddTransient<ConfigurationSampleService>();
        services.AddTransient<EnvironmentSampleService>();
    }).Build();

var service = host.Services.GetRequiredService<ConfigurationSampleService>();
service.ShowConfiguration();
service.ShowTypedConfiguration();
service.ShowCustomConfiguration();

var service2 = host.Services.GetRequiredService<EnvironmentSampleService>();
service2.ShowHostEnvironment();
