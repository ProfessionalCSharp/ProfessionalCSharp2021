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
        Menu? menu = await _menusContext.Menus.FindAsync(id);
        Console.WriteLine(menu);
        Console.WriteLine();
    }

    public async Task SingleOrDefaultAsync(string text)
    {
        Console.WriteLine(nameof(SingleOrDefaultAsync));
        Menu? menu = await _menusContext.Menus.TagWith("SingleOrDefault").SingleOrDefaultAsync(m => m.Text == text);
        Console.WriteLine(menu);
        Console.WriteLine();
    }

    public async Task FirstOrDefaultAsync(string title)
    {
        Console.WriteLine(nameof(SingleOrDefaultAsync));
        Menu? menu = await _menusContext.Menus.TagWith("FirstOrDefault").FirstOrDefaultAsync(m => m.Text == title);
        Console.WriteLine(menu);
        Console.WriteLine();
    }

    public async Task WhereAsync()
    {
        Console.WriteLine(nameof(WhereAsync));
        var menus = await _menusContext.Menus.Where(m => m.Text.Contains("menu")).TagWith("Where").ToListAsync();
        foreach (var menu in menus)
        {
            Console.WriteLine(menu);
        }
        Console.WriteLine();
    }

    public async Task PagingAsync(int skip, int take)
    {
        Console.WriteLine(nameof(PagingAsync));
        var menus = await _menusContext.Menus
            .OrderBy(m => m.MenuId)
            .Skip(skip)
            .Take(take)
            .TagWith("SkipAndTake")
            .ToListAsync();
        foreach (var menu in menus)
        {
            Console.WriteLine(menu);
        }

        Console.WriteLine();
    }

    public async Task GetAllMenusUsingAsyncStream()
    {
        IAsyncEnumerable<Menu> menus = _menusContext.Menus.AsAsyncEnumerable();
        await foreach (var menu in menus)
        {
            Console.WriteLine(menu);
        }
    }

    public async Task RawSqlAsync(string term)
    {
        Console.WriteLine(nameof(RawSqlAsync));
        var menus = await _menusContext.Menus.FromSqlInterpolated($"SELECT * FROM [mc].[Menus] WHERE TEXT = '{term}'").TagWith("RawSQL").ToListAsync();
        foreach (var menu in menus)
        {
            Console.WriteLine(menu);
        }

        Console.WriteLine();
    }

    public void UseCompiledQuery()
    {
        Console.WriteLine(nameof(UseCompiledQuery));
        var menus = _menusContext.MenusByText("menu 26");
        foreach (var menu in menus)
        {
            Console.WriteLine(menu);
        }
        Console.WriteLine();
    }

    public async Task UseCompiledQueryAsync()
    {
        Console.WriteLine(nameof(UseCompiledQueryAsync));

        await foreach (var menu in _menusContext.MenusByTextAsync("menu 26"))
        {
            Console.WriteLine(menu);
        }
        Console.WriteLine();
    }
    public async Task UseEFCunctions(string textSegment)
    {
        Console.WriteLine(nameof(UseEFCunctions));
        string likeExpression = $"%{textSegment}%";

        var menus = await _menusContext.Menus.Where(m => EF.Functions.Like(m.Text, likeExpression)).ToListAsync();
        foreach (var menu in menus)
        {
            Console.WriteLine(menu);
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
