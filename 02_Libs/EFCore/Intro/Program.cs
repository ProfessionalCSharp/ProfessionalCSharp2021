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
        services.AddScoped<Runner>();
    })
    .Build();

using var scope = host.Services.CreateScope();
var runner = scope.ServiceProvider.GetRequiredService<Runner>();

string input = Console.ReadLine()?.Trim().ToLower() ?? string.Empty;
if (input == "yes" || input == "y")
{
    await runner.ApplyMigrationsAsync();
}
else
{
    
}

await runner.CreateTheDatabaseAsync();
await runner.AddBookAsync("Professional C# and .NET", "Wrox Press");
await runner.AddBooksAsync();
await runner.ReadBooksAsync();
await runner.QueryBooksAsync();
await runner.UpdateBookAsync();
await runner.DeleteBooksAsync();
await runner.DeleteDatabaseAsync();
