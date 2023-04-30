using Codebreaker.Models;
using Codebreaker.Utilities;

namespace Codebreaker.Services;

public interface IGamesService
{
    Task<IEnumerable<Game>> GetGamesAsync();
    Task<Game?> GetGameAsync(Guid gameId);
    Task<Game> CreateGameAsync(GameType gameType, string playerName);
    Task<SetMoveResponse> SetMoveAsync(SetMoveRequest moveRequest);
}

public class GamesService : IGamesService
{
    private readonly IGamesRepository _gamesRepository;
    private readonly GamesFactory _gameFactory;

    public GamesService(GamesFactory gameFactory, IGamesRepository gamesRepository)
    {
        _gameFactory = gameFactory;
        _gamesRepository = gamesRepository;
    }

    public Task<Game> CreateGameAsync(GameType gameType, string playerName)
    {
        Game game = _gameFactory.CreateGame(gameType, playerName);
        _gamesRepository.AddGameAsync(game);
        return Task.FromResult(game);
    }

    public async Task<IEnumerable<Game>> GetGamesAsync()
    {
        var games = await _gamesRepository.GetGamesAsync();
        return games;
    }

    public async Task<SetMoveResponse> SetMoveAsync(SetMoveRequest moveRequest)
    {
        static void ValidateFieldsSet(int holes, int fieldsSet)
        {             
            if (fieldsSet != holes)
            {
                throw new GameException($"The game needs {holes} fields set") { HResult = 4201 };
            }
        }

        SetMoveResponse SetColorMove(Game<ColorField, ColorResult> game, IEnumerable<ColorField>? fields)
        {
            if (fields is null)
            {
                throw new GameException($"The game {game.GameType} needs `ColorFields` set") { HResult = 4202 };
            }
            ValidateFieldsSet(game.Holes, fields.Count());
            
            ColorResult result = game.GetResult(fields);
            Move<ColorField, ColorResult> move = new(game.GameId, Guid.NewGuid(), game.LastMove + 1, new List<ColorField>(fields), result);
            game.AddMove(move);
            return new SetMoveResponse(game.GameId, game.GameType, move.MoveNumber, ColorResult: move.Results);
        }

        SetMoveResponse SetSimpleMove(Game<ColorField, SimpleColorResult> game, IEnumerable<ColorField>? fields)
        {
            if (fields is null)
            {
                throw new GameException($"The game {game.GameType} needs `ColorFields` set") { HResult = 4203 };
            }
            ValidateFieldsSet(game.Holes, fields.Count());

            SimpleColorResult result = game.GetResult(fields);
            Move<ColorField, SimpleColorResult> move = new(game.GameId, Guid.NewGuid(), game.LastMove + 1, new List<ColorField>(fields), result);
            game.AddMove(move);
            return new SetMoveResponse(game.GameId, game.GameType,move.MoveNumber, SimpleResult: move.Results);
        }

        SetMoveResponse SetShapeMove(Game<ShapeAndColorField, ShapeAndColorResult> game, IEnumerable<ShapeAndColorField>? fields)
        {
            if (fields is null)
            {
                throw new GameException($"The game {game.GameType} needs `ColorFields` set") { HResult = 4204 };
            }
            ValidateFieldsSet(game.Holes, fields.Count());

            ShapeAndColorResult result = game.GetResult(fields);
            Move<ShapeAndColorField, ShapeAndColorResult> move = new(game.GameId, Guid.NewGuid(), game.LastMove + 1, new List<ShapeAndColorField>(fields), result);
            return new SetMoveResponse(game.GameId, game.GameType, move.MoveNumber, ShapeResult: result);
        }

        Game game = await _gamesRepository.GetGameAsync(moveRequest.GameId) ?? throw new GameException("Game id not found") { HResult = 4400 };

        SetMoveResponse response = game switch
        {
            Game<ColorField, ColorResult> colorGame => SetColorMove(colorGame, moveRequest.ColorFields),
            Game<ColorField, SimpleColorResult> simpleGame => SetSimpleMove(simpleGame, moveRequest.ColorFields),
            Game<ShapeAndColorField, ShapeAndColorResult> shapeGame => SetShapeMove(shapeGame, moveRequest.ShapeAndColorFields),
            _ => throw new GameException("game type not found")
        };

        await _gamesRepository.UpdateGameAsync(game.GameId, game);

        return response;
    }

    public async Task<Game?> GetGameAsync(Guid gameId)
    {
        var game = await _gamesRepository.GetGameAsync(gameId);
        return game;
    }
}
