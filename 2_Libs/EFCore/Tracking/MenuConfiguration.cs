using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using static ColumnNames;

internal class MenuConfiguration : IEntityTypeConfiguration<MenuItem>
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
            .WithMany(c => c.Menus)
            .HasForeignKey(MenuCardId);

        builder.Property<Guid>(RestaurantId);
    }
}

