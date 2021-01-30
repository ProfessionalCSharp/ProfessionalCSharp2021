using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var settings = context.Configuration;
        services.Configure<EchoServiceOptions>(settings.GetSection("Echoserver"));
        services.AddTransient<EchoServer>();
    })
    .Build();

var logger = host.Services.GetRequiredService<ILoggerFactory>().CreateLogger("EchoServer");

CancellationTokenSource cancellationTokenSource = new();

Console.CancelKeyPress += (sender, e) =>
{
    logger.LogInformation("cancellation initiated by the user");
    cancellationTokenSource.Cancel();
};

var service = host.Services.GetRequiredService<EchoServer>();
await service.StartListenerAsync(cancellationTokenSource.Token);

Console.ReadLine();
