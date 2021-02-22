using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.Property<int>("_id")
            .HasColumnName("Id")
            .IsRequired()
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Property(r => r.Name)
            .HasField("_name")
            .UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction)
            .HasMaxLength(30);

        builder.HasKey("_id");
    }
}
