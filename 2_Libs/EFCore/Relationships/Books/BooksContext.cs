public class BooksContext : DbContext
{
    public BooksContext(DbContextOptions<BooksContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("bk");

        modelBuilder.ApplyConfiguration(new PersonConfiguration());

        modelBuilder.Entity<Book>()
            .HasMany(b => b.Authors)
            .WithMany(a => a.WrittenBooks);

        // for data seeding with EF Core use the UsingEntity method with anonymous types
    }

    public DbSet<Book> Books => Set<Book>();
    public DbSet<Person> People => Set<Person>();
}
