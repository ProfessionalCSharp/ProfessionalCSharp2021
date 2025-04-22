﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

class Runner(IDbContextFactory<MenusContext> menusContextFactory)
{
    private readonly IDbContextFactory<MenusContext> _menusContextFactory = menusContextFactory;

    public async Task CreateDatabaseAsync()
    {
        using var context = _menusContextFactory.CreateDbContext();
        await context.Database.EnsureCreatedAsync();
    }

    public async Task AddRecordsAsync()
    {
        Console.WriteLine(nameof(AddRecordsAsync));
        using var context = _menusContextFactory.CreateDbContext();
        MenuCard soupCard = new("Soups");

        MenuItem[] soups =
        [
            new MenuItem("Consommé Célestine (with shredded pancake)")
            {
                Price = 4.8m,
                MenuCard = soupCard
            },
            new MenuItem("Baked Potato Soup")
            {
                Price = 4.8m,
                MenuCard = soupCard
            },
            new MenuItem("Cheddar Broccoli Soup")
            {
                Price = 4.8m,
                MenuCard = soupCard
            }
        ];

        foreach (var soup in soups)
        {
            soupCard.MenuItems.Add(soup);
        }

        context.MenuCards.Add(soupCard);

        ShowState(context);
        int records = await context.SaveChangesAsync();
        Console.WriteLine($"{records} added");
        Console.WriteLine();
    }

    private void ShowState(MenusContext context)
    {
        foreach (EntityEntry entry in context.ChangeTracker.Entries())
        {
            Console.WriteLine($"type: {entry.Entity.GetType().Name}, " +
              $"state: {entry.State}, {entry.Entity}");
        }
        Console.WriteLine();
    }

    public async Task ObjectTrackingAsync()
    {
        using var context = _menusContextFactory.CreateDbContext();
        Console.WriteLine(nameof(ObjectTrackingAsync));
        var m1 = await (from m in context.MenusItems
                        where m.Text.StartsWith("Con")
                        select m).FirstOrDefaultAsync();
        var m2 = await (from m in context.MenusItems
                        where EF.Functions.Like(m.Text, "%(%")
                        select m).FirstOrDefaultAsync();
        if (object.ReferenceEquals(m1, m2))
        {
            Console.WriteLine("the same object");
        }
        else
        {
            Console.WriteLine("not the same");
        }
        ShowState(context);

        Console.WriteLine();
    }

    public async Task UpdateRecordsAsync()
    {
        using var context = _menusContextFactory.CreateDbContext();
        MenuItem? menu = await context.MenusItems
          .Skip(1)
          .FirstOrDefaultAsync();
        if (menu is null)
        {
            Console.WriteLine("no menu available");
            return;
        }

        ShowState(context);
        menu.Price += 0.2m;
        ShowState(context);
        int records = await context.SaveChangesAsync();
        Console.WriteLine($"{records} updated");
        ShowState(context);
    }

    public async Task EfficientUpdateAsync()
    {
        using var context = _menusContextFactory.CreateDbContext();
        MenuItem? menu = await context.MenusItems
          .AsNoTracking()
          .Skip(1)
          .FirstOrDefaultAsync();
        if (menu is null)
        {
            Console.WriteLine("no menu available");
            return;
        }

        ShowState(context);
        menu.Price += 0.2m;
        ShowState(context);
        int records = await context.MenusItems.ExecuteUpdateAsync(
            m => m.SetProperty(m => m.Price, menu.Price)
        );
        Console.WriteLine($"{records} updated");
        ShowState(context);
    }


    public async Task UpdateRecordUntrackedAsync()
    {
        async Task<MenuItem> GetMenuItemAsync()
        {
            using var context = _menusContextFactory.CreateDbContext();
            return await context.MenusItems
                .OrderBy(m => m.MenuItemId)
                .Skip(2)
                .FirstAsync();
        }

        async Task UpdateMenuItemAsync(MenuItem menu)
        {
            using var context = _menusContextFactory.CreateDbContext();
            ShowState(context);
            // EntityEntry<Menu> entry = context.Menus.Attach(m);
            // entry.State = EntityState.Modified;
            context.MenusItems.Update(menu);
            ShowState(context);
            await context.SaveChangesAsync();
        }

        var menu = await GetMenuItemAsync();
        menu.Price += 0.7m;
        await UpdateMenuItemAsync(menu);
    }

    public async Task DeleteDatabaseAsync()
    {
        using var context = _menusContextFactory.CreateDbContext();
        Console.Write("Delete the database? (y|n) ");
        string? input = Console.ReadLine();
        if (input?.ToLower() == "y")
        {
            bool deleted = await context.Database.EnsureDeletedAsync();
            string deletionInfo = deleted ? "deleted" : "not deleted";
            Console.WriteLine($"database {deletionInfo}");
        }
    }
}

