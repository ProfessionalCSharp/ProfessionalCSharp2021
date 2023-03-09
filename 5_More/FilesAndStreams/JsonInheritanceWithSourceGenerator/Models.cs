using System.Text.Json.Serialization;

namespace JsonInheritance;
public record class Game(Guid GameId, string GameType, string PlayerName)
{
    // init modifier required for JSON deserialization
    [JsonInclude]
    public ICollection<Move> Moves { get; init; } = new List<Move>();

    public override string ToString()
    {
        string movesText = string.Join(":", Moves);
        return $"""
        {GameId}: {GameType}, {PlayerName}
        {movesText}
        """;
    }
}

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$discriminator")]
[JsonDerivedType(typeof(Move<ColorField>), typeDiscriminator: "color")]
[JsonDerivedType(typeof(Move<ShapeColorField>), typeDiscriminator: "shape")]
public abstract record class Move(Guid GameId, Guid MoveId, int MoveNumber);
public record class Move<TField>(Guid GameId, Guid MoveId, int MoveNumber)
    : Move(GameId, MoveId, MoveNumber)
{

    public required ICollection<TField> Fields { get; init; }

    public override string ToString()
    {
        return string.Join(":", Fields);
    }
}

public record class ColorField(string Color)
{
    public override string ToString() => $"{Color}";
}

public record class ShapeColorField(string Shape, string Color)
{
    public override string ToString() => $"{Shape} {Color}";
}
