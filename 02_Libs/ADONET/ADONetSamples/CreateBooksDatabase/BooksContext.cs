using Microsoft.EntityFrameworkCore;
using System;

namespace CreateBooksDatabase
{
    public class BooksContext : DbContext
    {

        public BooksContext(DbContextOptions<BooksContext> options)
            : base(options) {  }

        public DbSet<Book> Books => Set<Book>("Books");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Book b1 = new(1, "Professional C# (Beta 2 Edition)", "Wrox Press", "978-1861004994", new DateTime(2001, 6, 1));
            Book b2 = new(2, "Beginning C#", "Wrox Press", "978-1861004987", new DateTime(2001, 9, 15));
            modelBuilder.Entity<Book>().HasData(b1, b2);
        }
    }
}
