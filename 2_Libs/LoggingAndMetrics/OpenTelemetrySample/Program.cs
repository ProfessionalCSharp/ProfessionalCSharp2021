using LoggingSample;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System;
using System.Diagnostics;

Activity.DefaultIdFormat = ActivityIdFormat.W3C;  // default since .NET 5

using var tracerProvider = Sdk.CreateTracerProviderBuilder()
    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("OpenTelemetrySample"))
    .AddSource("LoggingSample.DistributedTracing")
    .AddConsoleExporter()
    .Build();

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging((hostingContext, logging) =>
    {
        logging.ClearProviders();
        logging.AddFilter(level => level >= LogLevel.Trace);
        logging.AddOpenTelemetry(options => options.AddConsoleExporter());

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
