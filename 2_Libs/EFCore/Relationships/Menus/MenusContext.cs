public class MenusContext : DbContext
{
    public MenusContext(DbContextOptions<MenusContext> options) 
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("ms");

        modelBuilder.Entity<MenuItem>()
          .HasOne<MenuDetails>(m => m.Details)
          .WithOne(d => d.MenuItem)
          .HasForeignKey<MenuDetails>(d => d.MenuDetailsId);

        modelBuilder.Entity<MenuItem>()
            .Property(b => b.Price)
            .HasColumnType("money");

        modelBuilder.Entity<MenuItem>().ToTable("MenuItems");
        modelBuilder.Entity<MenuDetails>().ToTable("MenuItems");
    }

    public DbSet<MenuItem> MenuItems => Set<MenuItem>();
    public DbSet<MenuDetails> MenuDetails => Set<MenuDetails>();
}
