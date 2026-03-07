global using Microsoft.EntityFrameworkCore;
global using static ColumnNames;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MenusConnection");
builder.Services.AddDbContext<MenusContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddScoped<Runner>();

var host = builder.Build();

await using var scope = host.Services.CreateAsyncScope();
var runner = scope.ServiceProvider.GetRequiredService<Runner>();
await runner.CreateDatabaseAsync();
