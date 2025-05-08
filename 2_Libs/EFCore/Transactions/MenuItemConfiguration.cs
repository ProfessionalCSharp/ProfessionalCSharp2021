﻿namespace TransactionsSamples;

internal class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> builder)
    {
        builder.ToTable("MenuItems")
            .HasKey(m => m.MenuItemId);
        builder.Property(m => m.MenuItemId)
            .ValueGeneratedOnAdd();
        builder.Property(m => m.Text)
            .HasMaxLength(50);
        builder.Property(m => m.Price)
            .HasColumnType("Money");

        builder.HasOne(m => m.MenuCard)
            .WithMany(c => c.MenuItems)
            .HasForeignKey(c => c.MenuCardId);

        builder.Property<Guid>(RestaurantId);
    }
}
