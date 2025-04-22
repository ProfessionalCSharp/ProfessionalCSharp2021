using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TemporalTableSample;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<BooksContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("BooksConnection");
    options.UseSqlServer(connectionString);
    options.EnableSensitiveDataLogging();
});
builder.Services.AddScoped<Runner>();

using var host = builder.Build();

using var scope = host.Services.CreateScope();
var runner = scope.ServiceProvider.GetRequiredService<Runner>();

await runner.CreateTheDatabaseAsync();
await runner.AddBookAsync("Professional C# and .NET", "Wrox Press");
await runner.AddBooksAsync();
await runner.ReadBooksAsync();
await runner.QueryBooksAsync();
await runner.UpdateBookAsync();
await runner.TemporalPointInTimeQueryAsync();
await runner.TemporalAllQueryAsync();
await runner.DeleteBooksAsync();
await runner.DeleteDatabaseAsync();
