using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
