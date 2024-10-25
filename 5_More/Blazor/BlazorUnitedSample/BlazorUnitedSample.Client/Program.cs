using BlazorUnitedSample.Client.Services;
using BlazorUnitedSample.Services;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddTransient<IBooksService, BooksAPIClient>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


//builder.Services.AddHttpClient<IBooksService, BooksAPIClient>(configure =>
//    configure.BaseAddress = new Uri(builder.Configuration.GetRequired("GameServiceBaseAddress"))
//);

await builder.Build().RunAsync();
