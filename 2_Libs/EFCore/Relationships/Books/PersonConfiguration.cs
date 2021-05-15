using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.OwnsOne(p => p.BusinessAddress, builder =>
        {
            builder.Property(a => a!.LineOne).HasColumnName("AddressLineOne").HasMaxLength(50);
            builder.Property(a => a!.LineTwo).HasColumnName("AddressLineTwo").HasMaxLength(50);
            builder.OwnsOne(a => a!.Location, locationBuilder =>
            {
                locationBuilder.Property(l => l!.City).HasColumnName("BusinessCity").HasMaxLength(30);
                locationBuilder.Property(l => l!.Country).HasColumnName("BusinessCountry").HasMaxLength(30);
            });
        });

        builder.OwnsOne(p => p.PrivateAddress, builder =>
        {
            builder.ToTable("PrivateAddresses");
            builder.Property(a => a!.LineOne).HasMaxLength(50);
            builder.Property(a => a!.LineTwo).HasMaxLength(50);
            builder.OwnsOne(a => a!.Location, builder =>
            {
                builder.Property(a => a!.City).HasColumnName("City").HasMaxLength(30);
                builder.Property(a => a!.Country).HasColumnName("Country").HasMaxLength(30);
            });
        });
    }
}
