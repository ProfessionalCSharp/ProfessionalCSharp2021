using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ColumnNames;

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

    public async Task DeleteMenuAsync(int id)
    {
        Menu? menu = await _menusContext.Menus.FindAsync(id);
        if (menu is null) return;

        _menusContext.Remove(menu);
        int records = await _menusContext.SaveChangesAsync();
        Console.WriteLine($"{records} deleted");
    }

    public async Task QueryDeletedMenusAsync()
    {
        IEnumerable<Menu> deletedMenus =
          await _menusContext.Menus
            .Where(b => EF.Property<bool>(b, IsDeleted))
            .ToListAsync();

        foreach (var menu in deletedMenus)
        {
            Console.WriteLine($"deleted: {menu}");
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

