using System.Text.Json.Serialization;

namespace Codebreaker.Models;

public record CreateGameRequest(string GameType, string PlayerName);

public record InvalidGameRequest(string Message, string[] Information);

public record CreateGameResponse(Guid GameId, string GameType, string PlayerName);

// depending on the game type, set ColorFields or ShapeAndColorFields
public record SetMoveRequest(Guid GameId, string GameType, int MoveNumber)
{
    public ICollection<ColorField>? ColorFields { get; set; }
    public ICollection<ShapeAndColorField>? ShapeAndColorFields { get; set; }
}

public record SetMoveResponse(
    Guid GameId, 
    string GameType, 
    int MoveNumber, 
    ColorResult? ColorResult = default, 
    ShapeAndColorResult? ShapeResult = default, 
    SimpleColorResult? SimpleResult = default);
