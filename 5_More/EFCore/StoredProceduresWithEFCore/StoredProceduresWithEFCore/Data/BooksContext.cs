using Microsoft.EntityFrameworkCore;

namespace StoredProceduresWithEFCore.Data;

public partial class BooksContext(DbContextOptions<BooksContext> options) : DbContext(options)
{
    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<MenuItem> MenuItems { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<PrivateAddress> PrivateAddresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("Books", "bk");

            entity.Property(e => e.Publisher).HasMaxLength(30);
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.ToTable("MenuItems", "ms");

            entity.Property(e => e.Price).HasColumnType("money");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.ToTable("Payments", "bank");

            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.ToTable("People", "bk");

            entity.Property(e => e.AddressLineOne).HasMaxLength(50);
            entity.Property(e => e.AddressLineTwo).HasMaxLength(50);
            entity.Property(e => e.BusinessAddressLocationCity).HasColumnName("BusinessAddress_Location_City");
            entity.Property(e => e.BusinessAddressLocationCountry).HasColumnName("BusinessAddress_Location_Country");

            entity.HasMany(d => d.WrittenBooksBooks).WithMany(p => p.AuthorsPeople)
                .UsingEntity<Dictionary<string, object>>(
                    "BookPerson",
                    r => r.HasOne<Book>().WithMany().HasForeignKey("WrittenBooksBookId"),
                    l => l.HasOne<Person>().WithMany().HasForeignKey("AuthorsPersonId"),
                    j =>
                    {
                        j.HasKey("AuthorsPersonId", "WrittenBooksBookId");
                        j.ToTable("BookPerson", "bk");
                        j.HasIndex(new[] { "WrittenBooksBookId" }, "IX_BookPerson_WrittenBooksBookId");
                    });
        });

        modelBuilder.Entity<PrivateAddress>(entity =>
        {
            entity.HasKey(e => e.PersonId);

            entity.ToTable("PrivateAddresses", "bk");

            entity.Property(e => e.PersonId).ValueGeneratedNever();
            entity.Property(e => e.City).HasMaxLength(30);
            entity.Property(e => e.Country).HasMaxLength(30);
            entity.Property(e => e.LineOne).HasMaxLength(50);
            entity.Property(e => e.LineTwo).HasMaxLength(50);

            entity.HasOne(d => d.Person).WithOne(p => p.PrivateAddress).HasForeignKey<PrivateAddress>(d => d.PersonId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    public async Task<IEnumerable<Book>> GetBooksByYearAsync(int year)
    {
        var yearParam = new Microsoft.Data.SqlClient.SqlParameter("@Year", year);
        // Execute the stored procedure and map the results to the Book entity
        // multiple results are not supported with EF Core https://github.com/dotnet/efcore/issues/8127
        // https://github.com/dotnet/efcore/issues/8127#issuecomment-1621907584 - extension method for multiple result sets
        // workaround is to use a stored procedure with a single result set
        // or ADO.NET directly

        var books = await Books
            .FromSqlInterpolated($"EXEC[bk].[GetBooksByYear] {year}")
            .ToListAsync();
        return books;
    }
}
