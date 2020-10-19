using ConfigurationSample;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

using var host = Host.CreateDefaultBuilder()
    .ConfigureAppConfiguration(config =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory());
        config.AddJsonFile("customconfigurationfile.json", optional: true);

    }).ConfigureServices(services =>
    {
        services.AddTransient<SampleService>();
        services.AddTransient<SampleService2>();
    }).Build();

var service = host.Services.GetRequiredService<SampleService>();
service.ShowConfiguration();
service.ShowTypedConfiguration();
service.ShowCustomConfiguration();

var service2 = host.Services.GetRequiredService<SampleService2>();
service2.ShowHostEnvironment();

