using Microsoft.EntityFrameworkCore;

public class BooksContext : DbContext
{
    public BooksContext(DbContextOptions<BooksContext> options)
        : base(options) { }

    public DbSet<Book> Books => Set<Book>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().Property(b => b.Publisher).HasMaxLength(20).IsRequired(false);

        Book b1 = new(1, "Professional C# 6 and .NET Core", "Wrox Press");
        Book b2 = new(2, "Professional C# 7 and .NET Core 2.0", "Wrox Press");
        Book b3 = new(3, "Professional C# and .NET - 2021 Edition", "Wrox Press");
        modelBuilder.Entity<Book>().HasData(b1, b2, b3);
    }
}
