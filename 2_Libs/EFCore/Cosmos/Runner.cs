using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

internal class RestaurantConfiguration
{
    public string? RestaurantId { get; set; }
}

internal class Runner
{
    private readonly string _restaurantId;
    private readonly MenusContext _menusContext;

    public Runner(MenusContext menusContext, IOptions<RestaurantConfiguration> options)
    {
        _menusContext = menusContext;
        _restaurantId = options.Value.RestaurantId ?? throw new ArgumentException("restaurant-id required");
    }

    public async Task CreateDatabaseAsync()
    {
        await _menusContext.Database.EnsureCreatedAsync();
    }

    public async Task AddMenuCardAsync()
    {
        Console.WriteLine(nameof(AddMenuCardAsync));
        MenuCard soupCard = new("Soups", _restaurantId);

        Menu[] soups = new[]
        {
            new Menu("Consommé Célestine (with shredded pancake)")
            {
                Price = 4.8m
            },
            new Menu("Baked Potato Soup")
            {
                Price = 4.8m
            },
            new Menu("Cheddar Broccoli Soup")
            {
                Price = 4.8m
            }
        };

        foreach (var soup in soups)
        {
            soupCard.Menus.Add(soup);
        }

        _menusContext.MenuCards.Add(soupCard);

        int records = await _menusContext.SaveChangesAsync();
        Console.WriteLine($"{records} added");
        Console.WriteLine();
    }

    public async Task AddAddtionalCardsAsync()
    {
        Random random = new();
        var menus = Enumerable.Range(1, 10).Select(i => new Menu($"menu {i}") { Price = random.Next(8) }).ToList();
        var cards = Enumerable.Range(1, 5).Select(i => new MenuCard($"card {i}", _restaurantId) { Menus = menus });

        await _menusContext.MenuCards.AddRangeAsync(cards);
        await _menusContext.SaveChangesAsync();
    }

    public async Task ShowCardsAsync()
    {
        var cards = await _menusContext.MenuCards
            .Where(c => c.IsActive)
            .Where(c => c.Title == "Soups")
            .WithPartitionKey(_restaurantId)
            .ToListAsync();
        foreach (var card in cards)
        {
            Console.WriteLine(card.Title);
            foreach (var menu in card.Menus)
            {
                Console.WriteLine(menu.Text);
            }
        }
    }

    public async Task DeleteDatabaseAsync()
    {
        Console.Write("Delete the database? (y|n) ");
        string? input = Console.ReadLine();
        if (input?.ToLower() == "y")
        {
            bool deleted = await _menusContext.Database.EnsureDeletedAsync();
            string deletionInfo = deleted ? "deleted" : "not deleted";
            Console.WriteLine($"database {deletionInfo}");
        }
    }


}

