using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MenusConnection");
builder.Services.AddDbContextFactory<MenusContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<Runner>();

var host = builder.Build();

using var scope = host.Services.CreateScope();
var runner = scope.ServiceProvider.GetRequiredService<Runner>();
await runner.CreateDatabaseAsync();

await runner.AddRecordsAsync();
await runner.ObjectTrackingAsync();
await runner.UpdateRecordsAsync();
await runner.UpdateRecordUntrackedAsync();
await runner.EfficientUpdateAsync();

await runner.DeleteDatabaseAsync();
