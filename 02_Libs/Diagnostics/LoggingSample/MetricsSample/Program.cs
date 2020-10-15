using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging =>
    {
        // add a logger named "EventSource"
        logging.AddEventSourceLogger();
    })
    .ConfigureServices(services =>
    {
        services.AddHttpClient<MainRunner>(client =>
        {

        }).AddTypedClient<MainRunner>();
        
    }).Build();

var runner = host.Services.GetRequiredService<MainRunner>();
await runner.RunAsync();
