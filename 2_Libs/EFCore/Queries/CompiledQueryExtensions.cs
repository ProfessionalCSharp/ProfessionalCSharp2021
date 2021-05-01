using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

static class CompiledQueryExtensions
{
    private static Func<MenusContext, string, IEnumerable<MenuItem>>? s_menuItemsByText;

    private static Func<MenusContext, string, IEnumerable<MenuItem>> CompileMenusByTextQuery() 
        => EF.CompileQuery((MenusContext context, string text)
                => context.MenuItems.Where(m => m.Text == text));

    public static IEnumerable<MenuItem> MenuItemsByText(this MenusContext menusContext, string text)
    {
        if (s_menuItemsByText is null)
        {
            s_menuItemsByText = CompileMenusByTextQuery();
        }
        return s_menuItemsByText(menusContext, text);
    }

    private static Func<MenusContext, string, IAsyncEnumerable<MenuItem>>? s_menuItemsByTextAsync;
    private static Func<MenusContext, string, IAsyncEnumerable<MenuItem>> CompileMenuItemsByTextAsyncQuery() 
        => EF.CompileAsyncQuery((MenusContext context, string text)
            => context.MenuItems.Where(m => m.Text == text));

    public static IAsyncEnumerable<MenuItem> MenuItemsByTextAsync(this MenusContext menusContext, string text)
    {
        if (s_menuItemsByTextAsync is null)
        {
            s_menuItemsByTextAsync = CompileMenuItemsByTextAsyncQuery();
        }
        return s_menuItemsByTextAsync(menusContext, text);
    }
}
