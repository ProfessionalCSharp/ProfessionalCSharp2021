using LoggingSample;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System;
using System.Runtime.InteropServices;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging((context, logging) =>
    {
        logging.AddConfiguration(context.Configuration.GetSection("Logging"));
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            logging.AddEventLog(); // EventLogLoggerProvider
        }
        logging.AddEventSourceLogger();
        logging.AddSimpleConsole(config =>
        {
            config.IncludeScopes = true;
        });
        logging.AddSystemdConsole(configure =>
        {
            configure.IncludeScopes = true;
        });
        // builder.AddConsole(options => options.IncludeScopes = true);
        logging.AddDebug();

        logging.Configure(options =>
        {
            options.ActivityTrackingOptions = ActivityTrackingOptions.SpanId
                                                | ActivityTrackingOptions.TraceId
                                                | ActivityTrackingOptions.ParentId;
        });
    })
    .ConfigureServices(services =>
    {
        services.AddHttpClient<NetworkService>(client =>
        {
        }).AddTypedClient<NetworkService>();
        services.AddScoped<Runner>();
    }).Build();

var runner = host.Services.GetRequiredService<Runner>();
await runner.RunAsync();

Console.WriteLine("Bye... Press return to exit");
Console.ReadLine();
