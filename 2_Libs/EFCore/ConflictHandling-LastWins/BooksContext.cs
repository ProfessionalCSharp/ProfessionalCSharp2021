using Microsoft.EntityFrameworkCore;

public class BooksContext : DbContext
{
    public BooksContext(DbContextOptions<BooksContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var sampleBooks = GetSampleBooks();
        modelBuilder.Entity<Book>().HasData(sampleBooks);
        base.OnModelCreating(modelBuilder);
    }

    private IEnumerable<Book> GetSampleBooks()
        => Enumerable.Range(1, 100)
        .Select(id => new Book($"title {id}", "sample", id)).ToArray();

    public DbSet<Book> Books => Set<Book>();
}
