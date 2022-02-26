using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BooksAPI.Models
{
    public class BooksContext : DbContext
    {
        public BooksContext(DbContextOptions<BooksContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().Property(b => b.Title).HasMaxLength(50);
            modelBuilder.Entity<Book>().Property(b => b.Publisher).HasMaxLength(20);

            Book[] sampleBooks = Enumerable.Range(1, 100)
                .Select(i => new Book(i, $"title {i}", "sample pub"))
                .ToArray();
            modelBuilder.Entity<Book>().HasData(sampleBooks);
        }

        public DbSet<Book> Books => Set<Book>();
    }

    public record Book(int BookId, string Title, string Publisher);
}
