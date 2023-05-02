namespace Codebreaker.Models;

public enum GameType
{
    Game6x4,
    Game6x4Simple,
    Game8x5,
    Game5x5x4
}

public record CreateGameRequest(GameType GameType, string PlayerName);

public record InvalidGameRequest(string Message, string[] Information);

public record CreateGameResponse(Guid GameId, GameType GameType, string PlayerName);

// depending on the game type, set ColorFields or ShapeAndColorFields
public record SetMoveRequest(Guid GameId, GameType GameType, int MoveNumber)
{
    public ICollection<ColorField>? ColorFields { get; set; }
    public ICollection<ShapeAndColorField>? ShapeAndColorFields { get; set; }
}

public record SetMoveResponse(
    Guid GameId, 
    GameType GameType, 
    int MoveNumber, 
    ColorResult? ColorResult = default, 
    ShapeAndColorResult? ShapeResult = default, 
    SimpleColorResult? SimpleResult = default);
