using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InhertianceMappingTpCAndJSON.Models;
internal class MoveDataConfiguration<T> : IEntityTypeConfiguration<MoveData<T>>
    where T : class
{
    private readonly string _tableName;
    private readonly string _jsonColumnName;
    public MoveDataConfiguration(string tableName, string jsonColumnName)
    {
        _tableName = tableName;
        _jsonColumnName = jsonColumnName;
    }

    public void Configure(EntityTypeBuilder<MoveData<T>> builder)
    {
        builder.ToTable(_tableName);

        builder.HasOne<GameData>()
            .WithMany(g => (IEnumerable<MoveData<T>>)g.Moves)
            .HasForeignKey(m => m.GameId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsMany(m => m.Fields, navigationBuilder =>
        {
            navigationBuilder.ToJson(_jsonColumnName);
        });
    }
}
