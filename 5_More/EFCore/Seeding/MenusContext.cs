using Microsoft.Extensions.Logging;

namespace Seeding;

class MenusContext(DbContextOptions<MenusContext> options, IOptions<MenusContextOptions> menusContextOptions, ILogger<MenusContext> logger) : DbContext(options)
{
    public DbSet<MenuCard> MenuCards => Set<MenuCard>();
    public DbSet<MenuItem> MenuItems => Set<MenuItem>();
    public DbSet<Restaurant> Restaurants => Set<Restaurant>();
    public Guid RestaurantId => menusContextOptions.Value.RestaurantId;

    internal ILogger Logger => logger;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("mc")
            .ApplyConfiguration(new MenuCardConfiguration(RestaurantId))
            .ApplyConfiguration(new MenuItemConfiguration())
            .ApplyConfiguration(new RestaurantConfiguration());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ChangeTracker.DetectChanges();

        foreach (var item in ChangeTracker.Entries<MenuCard>()
            .Where(e => e.State is EntityState.Added))
        {
            item.CurrentValues[ColumnNames.RestaurantId] = RestaurantId;
        }

        foreach (var item in ChangeTracker.Entries<MenuItem>()
            .Where(e => e.State is EntityState.Added
            or EntityState.Modified
            or EntityState.Deleted))
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

public class MenusContextOptions
{
    public Guid RestaurantId { get; set; }
}

