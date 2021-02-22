using Microsoft.EntityFrameworkCore;

class OwnedEntitiesContext : DbContext
{
    public OwnedEntitiesContext(DbContextOptions<OwnedEntitiesContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
          .OwnsOne(p => p.CompanyAddress)
          .OwnsOne<Location>("Location", builder => 
          {
              builder.Property(p => p.City).HasColumnName("BusinessCity");
              builder.Property(p => p.Country).HasColumnName("BusinessCountry");
          });
        modelBuilder.Entity<Person>()
          .OwnsOne(p => p.PrivateAddress)
          .ToTable("Addr")
          .OwnsOne<Location>("Location");
    }

    public DbSet<Person> People => Set<Person>();

}

