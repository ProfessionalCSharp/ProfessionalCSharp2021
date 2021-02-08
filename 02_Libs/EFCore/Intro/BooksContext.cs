using Microsoft.EntityFrameworkCore;

public class BooksContext : DbContext
{
    public BooksContext(DbContextOptions<BooksContext> options)
        : base(options) { }

    public DbSet<Book> Books => Set<Book>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().Property(b => b.Publisher).HasMaxLength(20).IsRequired(false);

        Book b1 = new("Professional C# 6 and .NET Core", "Wrox Press", 1);
        Book b2 = new("Professional C# 7 and .NET Core 2.0", "Wrox Press", 2);
        Book b3 = new("Professional C# and .NET - 2021 Edition", "Wrox Press", 3);
        modelBuilder.Entity<Book>().HasData(b1, b2, b3);
    }
}
