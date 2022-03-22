using Microsoft.Extensions.Logging.EventLog;

using SimpleWorkerService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(options =>
    {
        if (OperatingSystem.IsWindows())
        {
            options.AddFilter<EventLogLoggerProvider>(level => level >= LogLevel.Information);
        }
    })
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();

        if (OperatingSystem.IsWindows())
        {
            services.Configure<EventLogSettings>(config =>
            {
                if (OperatingSystem.IsWindows())
                {
                    config.LogName = "Sample Service";
                    config.SourceName = "Sample Service Source";
                }
            });
        }
    })
    .UseWindowsService()
    .Build();

host.Run();
