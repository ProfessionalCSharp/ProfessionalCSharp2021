using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAppSample.Models
{
    public class MenuSamplesService
    {
        private List<MenuItem>? _menuItems;

        public IEnumerable<MenuItem> GetMenuItems() =>
            _menuItems ??= CreateMenuItems();

        private List<MenuItem> CreateMenuItems()
        {
            DateTime today = DateTime.Today;
            return Enumerable.Range(1, 10).Select(i => new MenuItem(i, $"menu {i}", 14.8, today.AddDays(i))).ToList();
        }
    }
}
