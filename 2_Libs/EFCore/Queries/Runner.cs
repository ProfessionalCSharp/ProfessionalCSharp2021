using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

class Runner
{
    private readonly MenusContext _menusContext;
    public Runner(MenusContext menusContext)
    {
        _menusContext = menusContext;
    }

    public Task CreateDatabaseAsync()
    {
        return _menusContext.Database.EnsureCreatedAsync();
    }

    public async Task FindByKeyAsync(int id)
    {
        Console.WriteLine(nameof(FindByKeyAsync));
        MenuItem? menuItem = await _menusContext.MenuItems.FindAsync(id);
        Console.WriteLine(menuItem);
        Console.WriteLine();
    }

    public async Task SingleOrDefaultAsync(string text)
    {
        Console.WriteLine(nameof(SingleOrDefaultAsync));
        MenuItem? menu = await _menusContext.MenuItems.TagWith("SingleOrDefault").SingleOrDefaultAsync(m => m.Text == text);
        Console.WriteLine(menu);
        Console.WriteLine();
    }

    public async Task FirstOrDefaultAsync(string title)
    {
        Console.WriteLine(nameof(SingleOrDefaultAsync));
        MenuItem? menu = await _menusContext.MenuItems.TagWith("FirstOrDefault").FirstOrDefaultAsync(m => m.Text == title);
        Console.WriteLine(menu);
        Console.WriteLine();
    }

    public async Task WhereAsync()
    {
        Console.WriteLine(nameof(WhereAsync));
        var menuItems = await _menusContext.MenuItems.Where(m => m.Text.Contains("menu")).TagWith("Where").ToListAsync();
        foreach (var menuItem in menuItems)
        {
            Console.WriteLine(menuItem);
        }
        Console.WriteLine();
    }

    public async Task PagingAsync(int skip, int take)
    {
        Console.WriteLine(nameof(PagingAsync));
        var menuItems = await _menusContext.MenuItems
            .OrderBy(m => m.MenuId)
            .Skip(skip)
            .Take(take)
            .TagWith("SkipAndTake")
            .ToListAsync();
        foreach (var menuItem in menuItems)
        {
            Console.WriteLine(menuItem);
        }

        Console.WriteLine();
    }

    public async Task GetAllMenusUsingAsyncStream()
    {
        IAsyncEnumerable<MenuItem> menuItems = _menusContext.MenuItems.AsAsyncEnumerable();
        await foreach (var menu in menuItems)
        {
            Console.WriteLine(menu);
        }
    }

    public async Task RawSqlAsync(string term)
    {
        Console.WriteLine(nameof(RawSqlAsync));
        var menuItems = await _menusContext.MenuItems.FromSqlInterpolated($"SELECT * FROM [mc].[Menus] WHERE TEXT = '{term}'").TagWith("RawSQL").ToListAsync();
        foreach (var menu in menuItems)
        {
            Console.WriteLine(menu);
        }

        Console.WriteLine();
    }

    public void UseCompiledQuery()
    {
        Console.WriteLine(nameof(UseCompiledQuery));
        var menuItems = _menusContext.MenuItemsByText("menu 26");
        foreach (var menu in menuItems)
        {
            Console.WriteLine(menu);
        }
        Console.WriteLine();
    }

    public async Task UseCompiledQueryAsync()
    {
        Console.WriteLine(nameof(UseCompiledQueryAsync));

        await foreach (var menuItem in _menusContext.MenuItemsByTextAsync("menu 26"))
        {
            Console.WriteLine(menuItem);
        }
        Console.WriteLine();
    }
    public async Task UseEFCunctions(string textSegment)
    {
        Console.WriteLine(nameof(UseEFCunctions));
        string likeExpression = $"%{textSegment}%";

        var menuItems = await _menusContext.MenuItems.Where(m => EF.Functions.Like(m.Text, likeExpression)).ToListAsync();
        foreach (var menuItem in menuItems)
        {
            Console.WriteLine(menuItem);
        }
        Console.WriteLine();
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
