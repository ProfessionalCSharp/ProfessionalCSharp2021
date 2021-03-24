using BookServiceClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var bookApiSettings = context.Configuration.GetSection("BooksService");
        services.Configure<BooksApiClientOptions>(bookApiSettings);
        services.AddHttpClient<BooksApiClient>(config =>
        {
            var baseAddress = context.Configuration.GetSection("BooksService")["BaseAddress"] ?? "https://localhost:5001";
            config.BaseAddress = new Uri(baseAddress);
        });
    }).Build();

Console.WriteLine("Client - press return to continue");
Console.ReadLine();

using var scope = host.Services.CreateScope();

var client = scope.ServiceProvider.GetRequiredService<BooksApiClient>();
await client.ReadChaptersAsync();
await client.ReadChapterAsync();
await client.AddChapterAsync();
await client.UpdateChapterAsync();
await client.RemoveChapterAsync();