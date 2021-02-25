using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var connectionString = context.Configuration.GetConnectionString("BooksConnection");
        services.AddDbContext<BooksContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        services.AddScoped<BooksRunner>();
    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    var booksRunner = scope.ServiceProvider.GetRequiredService<BooksRunner>();

    await booksRunner.CreateTheDatabaseAsync();

}

