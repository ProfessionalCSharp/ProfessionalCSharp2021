using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.OwnsOne(p => p.BusinessAddress, builder =>
        {
            builder.Property(a => a!.LineOne).HasColumnName("AddressLineOne");
            builder.Property(a => a!.LineTwo).HasColumnName("AddressLineTwo");
            builder.OwnsOne(a => a!.Location, locationBuilder =>
            {
                locationBuilder.Property(l => l!.City).HasColumnName("BusinessCity");
                locationBuilder.Property(l => l!.Country).HasColumnName("BusinessCountry");
            });
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
