using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static ColumnNames;

internal class MenuCardConfiguration : IEntityTypeConfiguration<MenuCard>
{
    public void Configure(EntityTypeBuilder<MenuCard> builder)
    {
        builder.ToTable("MenuCards")
            .HasKey(c => c.MenuCardId);

        builder.Property(c => c.MenuCardId)
            .ValueGeneratedOnAdd();
        builder.Property(c => c.Title)
            .HasMaxLength(50);
        builder.HasMany(c => c.MenuItems)
            .WithOne(m => m.MenuCard);

        // shadow property
        builder.Property<Guid>(RestaurantId);
    }
}
