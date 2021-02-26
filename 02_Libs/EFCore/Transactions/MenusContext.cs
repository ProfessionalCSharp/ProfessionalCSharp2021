using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static ColumnNames;

class MenusContext : DbContext
{
    public MenusContext(DbContextOptions<MenusContext> options)
        : base(options) {}

    public DbSet<MenuCard> MenuCards => Set<MenuCard>();
    public DbSet<Menu> Menus => Set<Menu>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("mc")
            .ApplyConfiguration(new MenuCardConfiguration())
            .ApplyConfiguration(new MenuConfiguration())
            .ApplyConfiguration(new RestaurantConfiguration());

        var restaurantId = Guid.Parse("{FDCD4390-48AD-42F1-AC6A-596F56731795}");
        var cardId = Guid.Parse("{3F597E5A-695A-4AF3-9084-FA4DDB23AE24}");
        var card1 = new
        {
            MenuCardId = cardId,
            Title = "Soups",
            RestaurantId = restaurantId,
        };

        var menus = GetInitialMenus(card1, restaurantId);
        modelBuilder.Entity<MenuCard>().HasData(card1);
        modelBuilder.Entity<Menu>().HasData(menus);
    }

    private IEnumerable<object> GetInitialMenus(dynamic card, Guid restaurantId)
    {
        DateTime now = DateTime.Now;
        return Enumerable.Range(1, 20).Select(id => new
        {
            MenuId = Guid.NewGuid(),
            Text = $"menu {id}",
            Price = 6.5M,
            IsDeleted = false,
            LastUpdated = now,
            MenuCardId = card.MenuCardId,
            RestaurantId = restaurantId
        }).ToArray();
    }

}

