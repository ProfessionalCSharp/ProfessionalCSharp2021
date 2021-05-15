using Microsoft.EntityFrameworkCore;

namespace BookModels
{
    public class BooksContext : DbContext
    {
        public BooksContext(DbContextOptions<BooksContext> options)
            : base(options) { }

        public DbSet<Book> Books => Set<Book>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var books = new Book[]
            {
                new("Professional C# and .NET - 2021 Edition", "Wrox Press", 1),
                new("Professional C# 7 and .NET Core 2", "Wrox Press", 2),
                new("Professional C# 6 and .NET Core 1.0", "Wrox Press", 3),
                new("Professional C# 5.0 and .NET 4.5.1", "Wrox Press", 4),
                new("Enterprise Services with the .NET Framework", "AWL", 5)
            };
            modelBuilder.Entity<Book>().HasData(books);
        }
    }
}
