using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;


static class ComiledQueries
{
    private static Func<MenusContext, string, IQueryable<Menu>>? _menusByText;

    public static Func<MenusContext, string, IQueryable<Menu>> MenusByText
    {
        get => _menusByText ??= GetMenusByTextQuery();
    }

    private static Func<MenusContext, string, IQueryable<Menu>> GetMenusByTextQuery() =>
        EF.CompileQuery<MenusContext, string, IQueryable<Menu>>((context, text)
                => context.Menus.Where(m => m.Text == text));

}

