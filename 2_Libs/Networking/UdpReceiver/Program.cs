using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var settings = context.Configuration;
        services.Configure<ReceiverOptions>(settings.GetSection("UdpReceiver"));
        services.AddTransient<Receiver>();
    }).Build();

var receiver = host.Services.GetRequiredService<Receiver>();
await receiver.RunAsync();
