using DIWithConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

using var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;
        // services.AddOptions(); // already added from host
        services.AddGreetingService(configuration.GetSection("GreetingService"));
        services.AddSingleton<IGreetingService, GreetingService>();
        services.AddTransient<HomeController>();
    }).Build();

var controller = host.Services.GetRequiredService<HomeController>();
string result = controller.Hello("Katharina");
Console.WriteLine(result);

