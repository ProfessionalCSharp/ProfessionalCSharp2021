using Microsoft.Extensions.DependencyInjection;
using System;
using DIWithOptions;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        // services.AddOptions(); // already added from host
        services.AddGreetingService(options =>
        {
            options.From = "Christian";
        });
        services.AddSingleton<IGreetingService, GreetingService>();
        services.AddTransient<HomeController>();
    }).Build();

var controller = host.Services.GetRequiredService<HomeController>();
string result = controller.Hello("Katharina");
Console.WriteLine(result);

