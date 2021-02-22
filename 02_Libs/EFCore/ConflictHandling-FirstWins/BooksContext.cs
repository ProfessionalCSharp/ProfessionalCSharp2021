using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

public class BooksContext : DbContext
{
    public BooksContext(DbContextOptions<BooksContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var sampleBooks = GetSampleBooks();
        modelBuilder.Entity<Book>().HasData(sampleBooks);

        // shadow property
        modelBuilder.Entity<Book>().Property<byte[]>("Timestamp")
          .HasColumnType("timestamp")
          .IsRowVersion();

        //modelBuilder.Entity<Book>().IndexerProperty<byte[]>("TimeStamp")
        //    .HasColumnType("timestamp")
        //    .IsRowVersion()
        //    // .ValueGeneratedOnAddOrUpdate()
        //    .IsConcurrencyToken();
    }

    private IEnumerable<Book> GetSampleBooks()
        => Enumerable.Range(1, 100)
        .Select(id => new Book($"title {id}", "sample", id)).ToArray();

    public DbSet<Book> Books => Set<Book>();
}
