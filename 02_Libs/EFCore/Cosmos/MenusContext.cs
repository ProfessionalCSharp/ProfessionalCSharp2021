using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

class MenusContext : DbContext
{
    private readonly string _restaurantId;

    public MenusContext(DbContextOptions<MenusContext> options, IOptions<RestaurantConfiguration> restaurantOptions)
        : base(options) 
    {
        _restaurantId = restaurantOptions.Value.RestaurantId ?? throw new System.Exception("restaurantid required");
    }

    public DbSet<MenuCard> MenuCards => Set<MenuCard>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultContainer("menucards")
            .ApplyConfiguration(new MenuCardConfiguration());


        modelBuilder.Entity<MenuCard>().HasPartitionKey("RestaurantId");
        modelBuilder.Entity<MenuCard>().HasQueryFilter(c => c.RestaurantId == _restaurantId);

    }
}

