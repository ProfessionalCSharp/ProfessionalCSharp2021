using System.Text.Json.Serialization;

namespace Codebreaker.Models;

public record CreateGameRequest(string GameType, string PlayerName);

public record CreateGameResponse(Guid GameId, string GameType, string PlayerName);

// depending on the game type, set ColorFields or ShapeAndColorFields
public record SetMoveRequest(Guid GameId, string GameType, int MoveNumber)
{
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public ICollection<ColorField>? ColorFields { get; set; }

    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public ICollection<ShapeAndColorField>? ShapeAndColorFields { get; set; }
}

public record SetMoveResponse(
    Guid GameId, 
    string GameType, 
    int MoveNumber, 
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)] 
    ColorResult? ColorResult = default, 
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)] 
    ShapeAndColorResult? ShapeResult = default, 
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)] 
    SimpleColorResult? SimpleResult = default);
