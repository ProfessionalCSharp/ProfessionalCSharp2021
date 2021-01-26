using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var settings = context.Configuration;
        services.Configure<EchoServiceOptions>(settings.GetSection("Echoserver"));
        services.AddTransient<EchoServer>();
    })
    .Build();

var service = host.Services.GetRequiredService<EchoServer>();
service.StartListener();

Console.ReadLine();
