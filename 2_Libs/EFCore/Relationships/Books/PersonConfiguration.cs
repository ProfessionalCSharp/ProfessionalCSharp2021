using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.OwnsOne(p => p.BusinessAddress, addressbuilder =>
        {
            addressbuilder.Property(a => a.LineTwo)
                .HasColumnName("AddressLineOne")
                .HasMaxLength(50);
            addressbuilder.Property(a => a.LineTwo)
                .HasColumnName("AddressLineTwo")
                .HasMaxLength(50);
            addressbuilder.OwnsOne(a => a.Location);
        });

        builder.OwnsOne(p => p.PrivateAddress, addressbuilder =>
        {
            addressbuilder.ToTable("PrivateAddresses");
            addressbuilder.Property(a => a.LineOne)
                .HasMaxLength(50);
            addressbuilder.Property(a => a.LineTwo)
                .HasMaxLength(50);
            addressbuilder.OwnsOne(a => a.Location, locationbuilder =>
            {
                locationbuilder.Property(a => a.City).HasColumnName("City").HasMaxLength(30);
                locationbuilder.Property(a => a.Country).HasColumnName("Country").HasMaxLength(30);
            });
        });
    }
}
