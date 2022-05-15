global using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DataSample;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var connectionString = context.Configuration.GetConnectionString("MySQLConnection");
        if (connectionString is null)
        {
            throw new InvalidOperationException("MySQLConnection is not configured");
        }
        ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);
        services.AddDbContext<BooksContext>(options =>
        {
            options.UseMySql(connectionString, serverVersion);
        });
        services.AddScoped<Runner>();
    })
    .Build();

await using var scope = host.Services.CreateAsyncScope();
var runner = scope.ServiceProvider.GetRequiredService<Runner>();

await runner.CreateTheDatabaseAsync();
await runner.AddBookAsync("Professional C# and .NET", "Wrox Press");
await runner.AddBooksAsync();
await runner.ReadBooksAsync();
await runner.QueryBooksAsync();
await runner.UpdateBookAsync();
await runner.DeleteBooksAsync();
await runner.DeleteDatabaseAsync();
