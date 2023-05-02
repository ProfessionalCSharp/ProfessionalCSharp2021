namespace Codebreaker.Models;

public abstract record class Game(Guid GameId, GameType GameType, string PlayerName, int Holes, int MaxMoves);

public record class Game<TField, TResult>(Guid GameId, GameType GameType, string PlayerName, int Holes, int MaxMoves)
    : Game(GameId, GameType, PlayerName, Holes, MaxMoves)
{
    internal Game(Guid GameId, GameType GameType, string PlayerName, int Holes, int MaxMoves, TField[] codes)
        : this(GameId, GameType, PlayerName, Holes, MaxMoves)
    {
        Codes = codes;
    }

    public ICollection<TField> Codes { get; } = new TField[Holes];
    internal readonly List<Move<TField, TResult>> _moves = new();
    public IEnumerable<Move<TField, TResult>> Moves => _moves;
    public int LastMove => _moves.Count;

    internal void AddMove(Move<TField, TResult> move)
    {
        _moves.Add(move);
    }
}

public abstract record class Move(Guid GameId, Guid MoveId, int MoveNumber);

public record class Move<TField, TResult>(Guid GameId, Guid MoveId, int MoveNumber)
    : Move(GameId, MoveId, MoveNumber)
{
    public Move(Guid GameId, Guid MoveId, int MoveNumber, ICollection<TField> fields, TResult? results)
        : this(GameId, MoveId, MoveNumber)
    {
        Fields = fields;
        Results = results;
    }

    public ICollection<TField>? Fields { get; private set; }
    public TResult? Results { get; init; }
}

public static class GameExtensions
{
    // calculate result - this is just a sample implementation returning dummy values
    public static ColorResult GetResult(this Game<ColorField, ColorResult> game, IEnumerable<ColorField> fields)
    {
        return game.GameType switch
        {
            GameType.Game6x4 => new ColorResult(1, 1),
            GameType.Game8x5 => new ColorResult(1, 2),
            _ => throw new InvalidOperationException()
        };
    }

    public static ShapeAndColorResult GetResult(this Game<ShapeAndColorField, ShapeAndColorResult> game, IEnumerable<ShapeAndColorField> fields)
    {
        return new ShapeAndColorResult(2, 1, 0);
    }

    public static SimpleColorResult GetResult(this Game<ColorField, SimpleColorResult> game, IEnumerable<ColorField> fields)
    {
        ResultInformation[] results = { ResultInformation.CorrectColor, ResultInformation.Incorrect, ResultInformation.Incorrect, ResultInformation.CorrectPositionAndColor };
        return new SimpleColorResult(results);
    }
}
