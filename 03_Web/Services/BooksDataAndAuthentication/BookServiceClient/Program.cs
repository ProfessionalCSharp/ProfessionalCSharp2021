using BookServiceClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var clientAuthenticationSettings = context.Configuration.GetSection("AzureAdB2C");
        services.Configure<ClientAuthenticationOptions>(clientAuthenticationSettings);
        services.AddSingleton<ClientAuthentication>();
        var bookApiSettings = context.Configuration.GetSection("BooksService");
        services.Configure<BooksApiClientOptions>(bookApiSettings);
        services.AddTransient<AuthenticationMessageHandler>();
        services.AddHttpClient<BooksApiClient>(config =>
        {
            var baseAddress = context.Configuration.GetSection("BooksService")["BaseAddress"] ?? "https://localhost:5001";
            config.BaseAddress = new Uri(baseAddress);
        }).AddHttpMessageHandler<AuthenticationMessageHandler>();
    }).Build();

Console.WriteLine("Client - press return to continue");
Console.ReadLine();

using var scope = host.Services.CreateScope();

var auth = scope.ServiceProvider.GetRequiredService<ClientAuthentication>();
await auth.LoginAsync();

var client = scope.ServiceProvider.GetRequiredService<BooksApiClient>();
await client.ReadChaptersAsync();
await client.ReadChapterAsync();
await client.ReadNotExistingChapterAsync();
await client.AddChapterAsync();
await client.UpdateChapterAsync();
await client.RemoveChapterAsync();