using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddTransient<Runner>();
    }).Build();

var runner = host.Services.GetRequiredService<Runner>();
runner.Init();
await runner.LoginAsync();
