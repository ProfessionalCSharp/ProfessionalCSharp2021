using InhertianceMappingTpCAndJSON.Models;

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

        modelBuilder.Entity<MoveData>().HasKey(m => m.MoveId);
        modelBuilder.Entity<MoveData>().ToTable("Moves");

        // a) TPH
        modelBuilder.Entity<MoveData>().UseTphMappingStrategy();

        modelBuilder.Entity<MoveData>().HasDiscriminator<string>("gameType")
            .HasValue<MoveData<ColorField>>("color")
            .HasValue<MoveData<ShapeAndColorField>>("shape");

        // The JSON columns cannot map to the same column name!
        modelBuilder.ApplyConfiguration(new MoveDataConfiguration<ColorField>("Moves", "ColorFields"));
        modelBuilder.ApplyConfiguration(new MoveDataConfiguration<ShapeAndColorField>("Moves", "ShapeFields"));

        // b) TPC - not supported with ToJSON yet: https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-7.0/whatsnew#json-columns

        //modelBuilder.Entity<MoveData>().UseTpcMappingStrategy();

        //modelBuilder.ApplyConfiguration(new MoveDataConfiguration<ColorField>("ShapeAndColorMoves"));
        //modelBuilder.ApplyConfiguration(new MoveDataConfiguration<ShapeAndColorField>("ColorMoves"));

    }

    public DbSet<GameData> Games { get; set; }
}
