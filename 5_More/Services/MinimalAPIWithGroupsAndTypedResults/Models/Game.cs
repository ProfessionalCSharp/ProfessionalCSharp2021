namespace Codebreaker.Models;

//public class GameTypes
//{
//    public const string Game6x4 = nameof(Game6x4);
//    public const string Game8x5 = nameof(Game8x5);
//    public const string Game5x5x4 = nameof(Game5x5x4);
//    public const string Game6x4Simple = nameof(Game6x4Simple);
//}

public abstract record class Game(Guid GameId, GameType GameType, string PlayerName, int Holes, int MaxMoves);

public record class Game<TField, TResult>(Guid GameId, GameType GameType, string PlayerName, int Holes, int MaxMoves)
    : Game(GameId, GameType, PlayerName, Holes, MaxMoves)
    where TResult: IParsable<TResult>
{
    public required ICollection<TField> Codes { get; init; }
    internal readonly List<Move<TField, TResult>> _moves = new();
    public IEnumerable<Move<TField, TResult>> Moves => _moves;
}

public abstract record class Move(Guid GameId, Guid MoveId, int MoveNumber);

public record class Move<TField, TResult>(Guid GameId, Guid MoveId, int MoveNumber)
    : Move(GameId, MoveId, MoveNumber)
{
    public required ICollection<TField> Fields { get; init; }
    public required TResult Results { get; init; }
}

public static class GameExtensions
{
    public static TResult AddMove<TField, TResult>(this Game<TField, TResult> game, IEnumerable<TField> fields)
        where TResult: IParsable<TResult>
    {
        int lastMove = 0;
        if (game._moves.Count > 0)
        {
            lastMove = game._moves.Last().MoveNumber;
        }

        // calculate result - TODO: based on the code list, this is just a dummy implementation returning sample values
        TResult result = game switch
        {
            { GameType: GameType.Game6x4 } => TResult.Parse("2:0", default),
            { GameType: GameType.Game8x5 } => TResult.Parse("1:2", default),
            { GameType: GameType.Game5x5x4 } => TResult.Parse("1:1:0", default),
            { GameType: GameType.Game6x4Simple } => TResult.Parse("0:1:1:2", default),
            _ => default,
        } ?? throw new InvalidOperationException();

        // create move
        Move<TField, TResult> move = new(game.GameId, Guid.NewGuid(), ++lastMove)
        {
            Fields = fields.ToArray(),
            Results = result
        };
        // add move to list
        game._moves.Add(move);
        return result;
    }
}
