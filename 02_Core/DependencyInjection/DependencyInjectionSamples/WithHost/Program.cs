using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WithHost;

using var host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddSingleton<IGreetingService, GreetingService>();
        services.AddTransient<HomeController>();
    }).Build();

var controller = host.Services.GetRequiredService<HomeController>();
string result = controller.Hello("Katharina");
Console.WriteLine(result);
