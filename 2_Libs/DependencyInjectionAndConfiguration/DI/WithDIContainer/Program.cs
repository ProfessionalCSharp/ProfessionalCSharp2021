using ServiceProvider container = GetServiceProvider();
var controller = container.GetRequiredService<HomeController>();
string result = controller.Hello("Stephanie");
Console.WriteLine(result);

static ServiceProvider GetServiceProvider()
{
    ServiceCollection services = new();
    services.AddSingleton<IGreetingService, GreetingService>();
    services.AddTransient<HomeController>();
    return services.BuildServiceProvider();
}
