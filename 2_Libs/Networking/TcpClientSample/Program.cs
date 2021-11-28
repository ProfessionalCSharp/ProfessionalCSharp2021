using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var settings = context.Configuration;
        services.Configure<QuotesClientOptions>(settings.GetSection("QuotesClient"));
        services.AddTransient<QuotesClient>();
    })
    .Build();

var logger = host.Services.GetRequiredService<ILoggerFactory>().CreateLogger("QuotesClient");

CancellationTokenSource cancellationTokenSource = new();

Console.CancelKeyPress += (sender, e) =>
{
    logger.LogInformation("cancellation initiated by the user");
    cancellationTokenSource.Cancel();
};

var client = host.Services.GetRequiredService<QuotesClient>();
await client.SendAndReceiveAsync(cancellationTokenSource.Token);
