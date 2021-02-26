using Microsoft.EntityFrameworkCore;

public class MenusContext : DbContext
{
    public MenusContext(DbContextOptions<MenusContext> options) 
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("ms");

        modelBuilder.Entity<Menu>()
          .HasOne<MenuDetails>(m => m.Details!)
          .WithOne(d => d.Menu!)
          .HasForeignKey<MenuDetails>(d => d.MenuDetailsId);
        modelBuilder.Entity<Menu>().ToTable("Menus");
        modelBuilder.Entity<MenuDetails>().ToTable("Menus");
    }

    public DbSet<Menu> Menus => Set<Menu>();
    public DbSet<MenuDetails> MenuDetails => Set<MenuDetails>();
}
