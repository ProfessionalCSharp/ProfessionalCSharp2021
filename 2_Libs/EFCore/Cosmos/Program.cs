using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var connectionString = context.Configuration.GetConnectionString("MenusConnection");
        var restaurantSettings = context.Configuration.GetSection("RestaurantConfiguration");

        services.Configure<RestaurantConfiguration>(restaurantSettings);
        services.AddDbContext<MenusContext>(options =>
        {
            options.UseCosmos(connectionString, "ProCSharpMenus1");
        });
        services.AddScoped<Runner>();
    })
    .Build();

using var scope = host.Services.CreateScope();
var runner = scope.ServiceProvider.GetRequiredService<Runner>();
await runner.CreateDatabaseAsync();

await runner.AddMenuCardAsync();
await runner.AddAddtionalCardsAsync();
await runner.ShowCardsAsync();
await runner.DeleteDatabaseAsync();
