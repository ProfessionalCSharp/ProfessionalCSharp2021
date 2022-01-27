using LoggingSample;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Diagnostics;

Activity.DefaultIdFormat = ActivityIdFormat.W3C;  // default since .NET 5

using var tracerProvider = Sdk.CreateTracerProviderBuilder()
    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("OpenTelemetrySample"))
    .AddSource(Runner.SourceName)
    .AddHttpClientInstrumentation()
    .AddConsoleExporter()
    .Build();


using var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddFilter(level => level >= LogLevel.Trace);
        logging.AddOpenTelemetry(options =>
        {
            options.AddConsoleExporter();
        });
    })
    .ConfigureServices(services =>
    {
        services.AddOpenTelemetryMetrics();
        services.AddOpenTelemetryTracing(builder =>
        {
            builder.AddConsoleExporter()
                .AddSource(Runner.SourceName)
                .SetResourceBuilder(
                    ResourceBuilder.CreateDefault()
                        .AddService(serviceName: "OpenTelemetrySample", serviceVersion: "1.0.0"))
                .AddHttpClientInstrumentation();
        });
        services.Configure<OpenTelemetryLoggerOptions>(options =>
        {
            options.IncludeScopes = true;
            options.ParseStateValues = true;
            options.IncludeFormattedMessage = true;
        });
        services.AddHttpClient<NetworkService>(client =>
        {
        }).AddTypedClient<NetworkService>();
        services.AddScoped<Runner>();
    }).Build();

var runner = host.Services.GetRequiredService<Runner>();
await runner.RunAsync();

Console.WriteLine("Bye... Press return to exit");
Console.ReadLine();
