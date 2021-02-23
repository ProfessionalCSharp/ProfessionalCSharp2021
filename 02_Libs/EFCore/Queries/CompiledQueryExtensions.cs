using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

static class CompiledQueryExtensions
{
    private static Func<MenusContext, string, IEnumerable<Menu>>? s_menusByText;

    private static Func<MenusContext, string, IEnumerable<Menu>> CompileMenusByTextQuery() =>
        EF.CompileQuery((MenusContext context, string text)
                => context.Menus.Where(m => m.Text == text));

    public static IEnumerable<Menu> MenusByText(this MenusContext menusContext, string text)
    {
        if (s_menusByText is null)
        {
            s_menusByText = CompileMenusByTextQuery();
        }
        return s_menusByText(menusContext, text);
    }

    private static Func<MenusContext, string, IAsyncEnumerable<Menu>>? s_menusByTextAsync;
    private static Func<MenusContext, string, IAsyncEnumerable<Menu>> CompileMenusByTextAsyncQuery() =>
        EF.CompileAsyncQuery((MenusContext context, string text)
            => context.Menus.Where(m => m.Text == text));

    public static IAsyncEnumerable<Menu> MenusByTextAsync(this MenusContext menusContext, string text)
    {
        if (s_menusByTextAsync is null)
        {
            s_menusByTextAsync = CompileMenusByTextAsyncQuery();
        }
        return s_menusByTextAsync(menusContext, text);
    }

}
