using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<MenusContextOptions>(builder.Configuration.GetSection("MenusContext"));

builder.Services.AddDbContext<MenusContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("MenusConnection") ?? throw new InvalidOperationException("Connection string 'MenusConnection' not found.");
    options.UseSqlServer(connectionString);
    options.UseSeeding((context, useManagement) =>
    {
        if (context is MenusContext menusContext)
        {
            var card = menusContext.MenuCards.FirstOrDefault();
            if (card is null)
            {
                var cards = GetInitialMenuCards(menusContext.RestaurantId);
                menusContext.MenuCards.AddRange(cards);
                int numberRecords = menusContext.SaveChanges();
                menusContext.Logger.LogInformation("Seeding {Number} records", numberRecords);
            }
        }
    });

    options.UseAsyncSeeding(async (context, useManagement, cancellationToken) =>
    {
        if (context is MenusContext menusContext)
        {
            var card = await menusContext.MenuCards.FirstOrDefaultAsync();
            if (card is null)
            { 
                var cards = GetInitialMenuCards(menusContext.RestaurantId);
                menusContext.MenuCards.AddRange(cards);
                int numberRecords = await menusContext.SaveChangesAsync(cancellationToken);
                menusContext.Logger.LogInformation("Seeding {Number} records", numberRecords);
            }
        }
    });

}, contextLifetime: ServiceLifetime.Scoped, optionsLifetime: ServiceLifetime.Scoped);
builder.Services.AddScoped<Runner>();

using var host = builder.Build();

using var scope = host.Services.CreateScope();
var runner = scope.ServiceProvider.GetRequiredService<Runner>();
await runner.CreateDatabaseAsync();
await runner.FindByKeyAsync(23);
await runner.SingleOrDefaultAsync("menu 27");
await runner.FirstOrDefaultAsync("menu 27");
await runner.WhereAsync();
await runner.PagingAsync(10, 5);
await runner.GetAllMenusUsingAsyncStream();
await runner.RawSqlAsync("menu 27");
runner.UseCompiledQuery();
await runner.UseCompiledQueryAsync();
await runner.UseEFunctions("24");

await runner.DeleteDatabaseAsync();

IEnumerable<MenuCard> GetInitialMenuCards(Guid restaurantId)
{
    MenuCard card1 = new("Soups");
    MenuCard card2 = new("Main");

    var menus1 = GetInitialMenus(card1, restaurantId, 1, 20);
    foreach (var menu in menus1)
    {
        card1.MenuItems.Add(menu);
    }
    var menus2 = GetInitialMenus(card2, restaurantId, 21, 20);
    foreach (var menu in menus2)
    {
        card2.MenuItems.Add(menu);
    }
    return [card1, card2];
}

IEnumerable<MenuItem> GetInitialMenus(MenuCard card, Guid restaurantId, int start, int count)
{
    return [.. Enumerable.Range(start, count).Select(id => new MenuItem($"menu {id}")
    {
        Price = 6.5M
    })];
}
