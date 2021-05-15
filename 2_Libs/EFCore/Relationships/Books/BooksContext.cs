using Microsoft.EntityFrameworkCore;

public class BooksContext : DbContext
{
    public BooksContext(DbContextOptions<BooksContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("bk");

        modelBuilder.ApplyConfiguration<Person>(new PersonConfiguration());

        InitData data = new();
        modelBuilder.Entity<Book>()
            .HasMany(b => b.Authors)
            .WithMany(a => a.WrittenBooks)
            .UsingEntity(ba => ba.HasData(data.GetBooksAuthors()));

        modelBuilder.Entity<Person>().HasData(data.GetAuthors());
        modelBuilder.Entity<Book>().HasData(data.GetBooks());
    }

    public DbSet<Book> Books => Set<Book>();
    public DbSet<Person> People => Set<Person>();
}
