using Books.Data;
using Books.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

using var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        string? connectionString = Environment.GetEnvironmentVariable("BooksConnection");
        if (connectionString is null) throw new InvalidOperationException("Configure the BooksConnection");

        services.AddDbContext<IBookChapterService, BooksContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    })
    .Build();

await host.RunAsync();
