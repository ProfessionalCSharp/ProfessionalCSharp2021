using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MenusConnection") ?? throw new InvalidOperationException("MenusConnection not configured");
var restaurantSettings = builder.Configuration.GetSection("RestaurantConfiguration");

builder.Services.Configure<RestaurantConfiguration>(options => 
    restaurantSettings.Bind(options));
builder.Services.AddDbContext<MenusContext>(options =>
{
    options.UseCosmos(connectionString, "ProCSharpMenus1");
});
builder.Services.AddScoped<Runner>();

var host = builder.Build();

await using var scope = host.Services.CreateAsyncScope();
var runner = scope.ServiceProvider.GetRequiredService<Runner>();
await runner.CreateDatabaseAsync();

await runner.AddMenuCardAsync();
await runner.AddAdditionalCardsAsync();
await runner.ShowCardsAsync();
await runner.DeleteDatabaseAsync();
