using var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory());
        config.AddJsonFile("customconfigurationfile.json", optional: true);
        config.AddJsonFile("dynamicchanges.json", optional: true, reloadOnChange: true);
    }).ConfigureServices(services =>
    {
        services.AddTransient<ConfigurationSampleService>();
        services.AddTransient<EnvironmentSampleService>();
    }).Build();

var service = host.Services.GetRequiredService<ConfigurationSampleService>();
service.ShowConfiguration();
service.ShowTypedConfiguration();
service.ShowCustomConfiguration();
service.ShowDynamicChangedValue();
service.ShowDynamicValue();

var service2 = host.Services.GetRequiredService<EnvironmentSampleService>();
service2.ShowHostEnvironment();

