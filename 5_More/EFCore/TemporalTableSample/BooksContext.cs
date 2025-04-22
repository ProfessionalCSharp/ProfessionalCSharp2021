using Microsoft.EntityFrameworkCore;

namespace TemporalTableSample;

public class BooksContext(DbContextOptions<BooksContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .ToTable("Books", b => b.IsTemporal());  // creates PeriodStart, PeriodEnd columns, use overload to customize the columns
    }

    public DbSet<Book> Books => Set<Book>();
}
