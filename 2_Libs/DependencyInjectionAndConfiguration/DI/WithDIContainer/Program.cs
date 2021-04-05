using Microsoft.Extensions.DependencyInjection;
using System;

using ServiceProvider container = GetServiceProvider();
var controller = container.GetRequiredService<HomeController>();
string result = controller.Hello("Stephanie");
Console.WriteLine(result);

static ServiceProvider GetServiceProvider()
{
    var services = new ServiceCollection();
    services.AddSingleton<IGreetingService, GreetingService>();
    services.AddTransient<HomeController>();
    return services.BuildServiceProvider();
}
