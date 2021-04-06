using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        IConfiguration configuration = context.Configuration;
        services.Configure<SenderOptions>(configuration.GetSection("UdpSender"));
        services.AddTransient<Sender>();
    }).Build();

var sender = host.Services.GetRequiredService<Sender>();
await sender.RunAsync();
