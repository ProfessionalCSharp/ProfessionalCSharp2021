using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.ServiceDiscovery;

using ServiceDiscoverySample;

Console.WriteLine("client - wait for service");
Console.ReadLine();


var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddConfigurationServiceEndpointProvider();
builder.Services.AddDnsServiceEndpointProvider();
builder.Services.AddDnsSrvServiceEndpointProvider();

builder.Services.Configure<ServiceDiscoveryOptions>(options =>
{
    
});
builder.Services.AddServiceDiscovery();

builder.Services.AddHttpClient<Runner>(client =>
{
    client.BaseAddress = new Uri("https+http://weather");
}).AddServiceDiscovery();

var app = builder.Build();
var runner = app.Services.GetRequiredService<Runner>();

await runner.ShowWeatherAsync();