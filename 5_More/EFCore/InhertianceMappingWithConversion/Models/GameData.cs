namespace Codebreaker.Models;

public record class GameData(Guid GameId, string GameType, string PlayerName)
{
    public ICollection<MoveData> Moves { get; } = new List<MoveData>();
}

public abstract record class MoveData(Guid GameId, Guid MoveId, int MoveNumber);
public record class MoveData<TField>(Guid GameId, Guid MoveId, int MoveNumber)
    : MoveData(GameId, MoveId, MoveNumber)
{
    public required ICollection<TField> Fields { get; init; }
}
