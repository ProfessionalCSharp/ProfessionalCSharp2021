using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class MenuCardConfiguration : IEntityTypeConfiguration<MenuCard>
{
    public void Configure(EntityTypeBuilder<MenuCard> builder)
    {
        builder.HasKey(c => c.MenuCardId);

        builder.Property(c => c.MenuCardId)
            .ValueGeneratedOnAdd();
        builder.Property(c => c.Title)
            .HasMaxLength(50);

        builder.OwnsMany(c => c.Menus);
    }
}
