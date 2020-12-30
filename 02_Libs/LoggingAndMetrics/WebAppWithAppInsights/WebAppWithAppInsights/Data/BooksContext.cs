using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WebAppWithAppInsights.Data
{
    public class BooksContext : DbContext
    {
        public BooksContext(DbContextOptions<BooksContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books => Set<Book>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().Property(b => b.Title).HasMaxLength(50);
            modelBuilder.Entity<Book>().Property(b => b.Publisher).HasMaxLength(25);

            var sampleBooks = Enumerable.Range(1, 100).Select(x => new Book { BookId = x, Title = $"book {x}", Publisher = "Sample Pub" }).ToArray();
            modelBuilder.Entity<Book>().HasData(sampleBooks);
        }
    }
}
