namespace ConfictHandling;

public class BooksContext(DbContextOptions<BooksContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var sampleBooks = GetSampleBooks();
        modelBuilder.Entity<Book>().HasData(sampleBooks);

        // shadow property
        modelBuilder.Entity<Book>().Property<byte[]>("Timestamp")
          .HasColumnType("timestamp")
          .IsRowVersion();
    }

    private static IEnumerable<Book> GetSampleBooks()
        => Enumerable.Range(1, 100)
        .Select(id => new Book($"title {id}", "sample", id)).ToArray();

    public DbSet<Book> Books => Set<Book>();
}
