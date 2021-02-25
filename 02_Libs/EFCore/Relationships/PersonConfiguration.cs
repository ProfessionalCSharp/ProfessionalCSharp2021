using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


internal class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.OwnsOne(p => p.BusinessAddress)
            .OwnsOne(a => a!.Location, builder =>
            {
                builder.Property(a => a!.City).HasColumnName("BusinessCity");
                builder.Property(a => a!.Country).HasColumnName("BusinessCountry");
            });
        builder.OwnsOne(p => p.PrivateAddress)
            .ToTable("PrivateAddresses")
            .OwnsOne(a => a!.Location, builder =>
            {
                builder.Property(a => a!.City).HasColumnName("City");
                builder.Property(a => a!.Country).HasColumnName("Country");
            });
    }
}
