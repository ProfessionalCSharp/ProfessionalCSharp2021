global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Options;

var builder = new HostApplicationBuilder(args);
builder.Services.AddGreetingService(builder.Configuration.GetSection("GreetingService"));
builder.Services.AddSingleton<IGreetingService, GreetingService>();
builder.Services.AddTransient<HomeController>();

using var host = builder.Build();

var controller = host.Services.GetRequiredService<HomeController>();
string result = controller.Hello("Katharina");
Console.WriteLine(result);
