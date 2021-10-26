using Microsoft.EntityFrameworkCore;

using static ColumnNames;

class MenusContext : DbContext
{
    public MenusContext(DbContextOptions<MenusContext> options)
        : base(options) {}

    public DbSet<MenuCard> MenuCards => Set<MenuCard>();
    public DbSet<MenuItem> MenuItems => Set<MenuItem>();
    public DbSet<Restaurant> Restaurants => Set<Restaurant>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("mc")
            .ApplyConfiguration(new MenuCardConfiguration())
            .ApplyConfiguration(new MenuItemConfiguration())
            .ApplyConfiguration(new RestaurantConfiguration());

        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        var restaurantId = Guid.NewGuid();
        var card1 = new
        {
            MenuCardId = 1,
            Title = "Soups",
            RestaurantId = restaurantId,
        };
        var card2 = new
        {
            MenuCardId = 2,
            Title = "Main",
            RestaurantId = restaurantId,
        };

        var menus1 = GetInitialMenus(card1, restaurantId, 1, 20);
        var menus2 = GetInitialMenus(card2, restaurantId, 21, 20);
        modelBuilder.Entity<MenuCard>().HasData(card1);
        modelBuilder.Entity<MenuItem>().HasData(menus1);
        modelBuilder.Entity<MenuCard>().HasData(card2);
        modelBuilder.Entity<MenuItem>().HasData(menus2);
    }

    private IEnumerable<dynamic> GetInitialMenus(dynamic card, Guid restaurantId, int start, int count)
    {
        DateTime now = DateTime.Now;
        return Enumerable.Range(start, count).Select(id => new
        {
            MenuItemId = id,
            Text = $"menu {id}",
            Price = 6.5M,
            IsDeleted = false,
            LastUpdated = now,
            MenuCardId = card.MenuCardId,
            RestaurantId = restaurantId
        }).ToArray();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ChangeTracker.DetectChanges();

        foreach (var item in ChangeTracker.Entries<MenuItem>()
            .Where(e => e.State == EntityState.Added
            || e.State == EntityState.Modified
            || e.State == EntityState.Deleted))
        {
            item.CurrentValues[LastUpdated] = DateTime.Now;

            if (item.State == EntityState.Deleted)
            {
                item.State = EntityState.Modified;
                item.CurrentValues[IsDeleted] = true;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}

