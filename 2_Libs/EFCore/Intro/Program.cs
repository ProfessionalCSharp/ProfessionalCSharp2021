global using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("BooksConnection");
builder.Services.AddDbContext<BooksContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddScoped<Runner>();

var host = builder.Build();

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
