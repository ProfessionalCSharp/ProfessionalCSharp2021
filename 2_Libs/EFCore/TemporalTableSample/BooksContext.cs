using Microsoft.EntityFrameworkCore;

public class BooksContext : DbContext
{
    public BooksContext(DbContextOptions<BooksContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().ToTable("Books", b => 
            b.IsTemporal());  // creates PeriodStart, PeriodEnd columns, use overload to customize the columns
    }


    public DbSet<Book> Books => Set<Book>();
}
