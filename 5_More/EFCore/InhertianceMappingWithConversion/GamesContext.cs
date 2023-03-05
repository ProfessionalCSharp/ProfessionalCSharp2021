using Microsoft.EntityFrameworkCore;

namespace Codebreaker.Models;

internal class GamesContext : DbContext
{
    public GamesContext(DbContextOptions<GamesContext> context)
        : base(context)
    {        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Codebreaker");

        modelBuilder.Entity<GameData>().ToTable("Games");
        modelBuilder.Entity<GameData>().HasKey(g => g.GameId);

        modelBuilder.Entity<GameData>().Property(g => g.GameType)
            .HasMaxLength(10)
            .IsRequired(true);

        modelBuilder.Entity<GameData>().Property(g => g.PlayerName)
            .HasMaxLength(20)
            .IsRequired(true);            

        modelBuilder.Entity<MoveData>().ToTable("Moves");
        modelBuilder.Entity<MoveData>().HasKey(m => m.MoveId);
        modelBuilder.Entity<MoveData>().HasOne<GameData>()
            .WithMany(g => g.Moves).HasForeignKey(m => m.GameId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<MoveData<ColorField>>()
            .HasOne<GameData>()
            .WithMany(g => (IEnumerable<MoveData<ColorField>>)g.Moves)
            .HasForeignKey(m => m.GameId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<MoveData<ShapeAndColorField>>()
            .HasOne<GameData>()
            .WithMany(g => (IEnumerable<MoveData<ShapeAndColorField>>)g.Moves)
            .HasForeignKey(m => m.GameId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<MoveData>().UseTphMappingStrategy();

        modelBuilder.Entity<MoveData>().Property<string>("Discriminator")
            .HasMaxLength(20)
            .IsRequired(true);

        modelBuilder.Entity<MoveData>().HasDiscriminator<string>("Discriminator")
            .HasValue<MoveData<ColorField>>("color")
            .HasValue<MoveData<ShapeAndColorField>>("shape");

        modelBuilder.Entity<MoveData<ShapeAndColorField>>()
        .Property(m => m.Fields)
        .HasColumnName("Fields")
        .HasColumnType("nvarchar")
        .HasMaxLength(150)
        .HasConversion(
            fields => fields.ToFieldString(),
            fields => 
                fields.ToFieldCollection<ShapeAndColorField>());

        modelBuilder.Entity<MoveData<ColorField>>()
            .Property(m => m.Fields)
            .HasColumnName("Fields")
            .HasColumnType("nvarchar")
            .HasMaxLength(150)
            .IsRequired(true)
            .HasConversion(
                fields => string.Join(':', fields),
                fields => 
                    fields.ToFieldCollection<ColorField>());
    }

    public DbSet<GameData> Games { get; set; }
}


public static class MappingExtensions
{
    public static ICollection<T> ToFieldCollection<T>(this string fields)
        where T : IParsable<T>
    {
        return fields.Split(':')
            .Select(field => T.Parse(field, default))
            .ToList();
    }

    public static string ToFieldString<T>(this ICollection<T> fields)
    {
        return string.Join(':', fields.Select(field => field.ToString()));
    }
}