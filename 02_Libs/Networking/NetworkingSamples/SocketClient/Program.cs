using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var settings = context.Configuration;
        services.Configure<EchoClientOptions>(settings.GetSection("EchoClient"));
        services.AddTransient<EchoClient>();
    })
    .Build();

var client = host.Services.GetRequiredService<EchoClient>();
await client.SendAndReceiveAsync();

Console.ReadLine();