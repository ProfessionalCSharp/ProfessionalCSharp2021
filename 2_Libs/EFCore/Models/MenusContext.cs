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
    public DbSet<Restaurant> Restaurants => Set<Restaurant>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.HasDefaultSchema("mc");
        //modelBuilder.Entity<Menu>().ToTable("Menus").HasKey(m => m.MenuId);
        //modelBuilder.Entity<Menu>().Property(m => m.MenuId).ValueGeneratedOnAdd();
        //modelBuilder.Entity<Menu>().Property(m => m.Text).HasMaxLength(50);
        //modelBuilder.Entity<Menu>().Property(m => m.Price).HasColumnType("Money");

        //modelBuilder.Entity<Menu>().HasOne(m => m.MenuCard)
        //    .WithMany(c => c.Menus)
        //    .HasForeignKey("MenuCardId");

        modelBuilder.HasDefaultSchema("mc")
            .ApplyConfiguration(new MenuCardConfiguration())
            .ApplyConfiguration(new MenuConfiguration())
            .ApplyConfiguration(new RestaurantConfiguration());

        var restaurantId = Guid.NewGuid();
        var card1 = new
        {
            MenuCardId = 1,
            Title = "Soups",
            RestaurantId = restaurantId,
        };

        var menus = GetInitialMenus(card1, restaurantId);
        modelBuilder.Entity<MenuCard>().HasData(card1);
        modelBuilder.Entity<Menu>().HasData(menus);
    }

    private IEnumerable<dynamic> GetInitialMenus(dynamic card, Guid restaurantId)
    {
        DateTime now = DateTime.Now;
        return Enumerable.Range(1, 20).Select(id => new
        {
            MenuId = id,
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

        foreach (var item in ChangeTracker.Entries<Menu>()
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

