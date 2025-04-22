﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

internal class RestaurantConfiguration
{
    public string? RestaurantId { get; set; }
}

internal class Runner(MenusContext menusContext, IOptions<RestaurantConfiguration> options)
{
    private readonly string _restaurantId = options.Value.RestaurantId ?? throw new ArgumentException("restaurant-id required");

    public async Task CreateDatabaseAsync()
    {
        await menusContext.Database.EnsureCreatedAsync();
    }

    public async Task AddMenuCardAsync()
    {
        Console.WriteLine(nameof(AddMenuCardAsync));
        MenuCard soupCard = new("Soups", _restaurantId);

        MenuItem[] soups =
        [
            new("Consommé Célestine (with shredded pancake)")
            {
                Price = 4.8m
            },
            new("Baked Potato Soup")
            {
                Price = 4.8m
            },
            new("Cheddar Broccoli Soup")
            {
                Price = 4.8m
            }
        ];

        foreach (var soup in soups)
        {
            soupCard.MenuItems.Add(soup);
        }

        menusContext.MenuCards.Add(soupCard);

        int records = await menusContext.SaveChangesAsync();
        Console.WriteLine($"{records} added");
        Console.WriteLine();
    }

    public async Task AddAdditionalCardsAsync()
    {
        Random random = new();
        var menus = Enumerable.Range(1, 10).Select(i => new MenuItem($"menu {i}") { Price = random.Next(8) }).ToList();
        var cards = Enumerable.Range(1, 5).Select(i => new MenuCard($"card {i}", _restaurantId) { MenuItems = menus });

        await menusContext.MenuCards.AddRangeAsync(cards);
        await menusContext.SaveChangesAsync();
    }

    public async Task ShowCardsAsync()
    {
        var cards = await menusContext.MenuCards
            .Where(c => c.IsActive)
            .Where(c => c.Title == "Soups")
            .WithPartitionKey(_restaurantId)
            .ToListAsync();
        foreach (var card in cards)
        {
            Console.WriteLine(card.Title);
            foreach (var menuItem in card.MenuItems)
            {
                Console.WriteLine(menuItem.Text);
            }
        }
    }

    public async Task DeleteDatabaseAsync()
    {
        Console.Write("Delete the database? (y|n) ");
        string? input = Console.ReadLine();
        if (input?.ToLower() == "y")
        {
            bool deleted = await menusContext.Database.EnsureDeletedAsync();
            string deletionInfo = deleted ? "deleted" : "not deleted";
            Console.WriteLine($"database {deletionInfo}");
        }
    }
}
