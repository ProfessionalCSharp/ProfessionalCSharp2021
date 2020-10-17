using Microsoft.Extensions.DependencyInjection;
using System;

using ServiceProvider container = RegisterServices();
var controller = container.GetRequiredService<HomeController>();
string result = controller.Hello("Stephanie");
Console.WriteLine(result);

static ServiceProvider RegisterServices()
{
    var services = new ServiceCollection();
    services.AddSingleton<IGreetingService, GreetingService>();
    services.AddTransient<HomeController>();
    return services.BuildServiceProvider();
}
