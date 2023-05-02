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
        static void VerifyFieldsSet(int holes, int fieldsSet)
        {             
            if (fieldsSet != holes)
            {
                throw new InvalidGameException($"The game needs {holes} fields set") { HResult = 4201 };
            }
        }

        SetMoveResponse SetColorMove(Game<ColorField, ColorResult> game, IEnumerable<ColorField>? fields)
        {
            if (fields is null)
            {
                throw new InvalidGameException($"The game {game.GameType} needs `ColorFields` set") { HResult = 4200 };
            }
            VerifyFieldsSet(game.Holes, fields.Count());
            
            ColorResult result = game.AddMove(fields);
            return new SetMoveResponse(game.GameId, game.GameType, game.Moves.Last().MoveNumber, ColorResult: result);
        }

        SetMoveResponse SetSimpleMove(Game<ColorField, SimpleColorResult> game, IEnumerable<ColorField>? fields)
        {
            if (fields is null)
            {
                throw new InvalidGameException($"The game {game.GameType} needs `ColorFields` set") { HResult = 4200 };
            }
            VerifyFieldsSet(game.Holes, fields.Count());

            SimpleColorResult result = game.AddMove(fields);
            return new SetMoveResponse(game.GameId, game.GameType, game.Moves.Last().MoveNumber, SimpleResult: result);
        }

        SetMoveResponse SetShapeMove(Game<ShapeAndColorField, ShapeAndColorResult> game, IEnumerable<ShapeAndColorField>? fields)
        {
            if (fields is null)
            {
                throw new InvalidGameException($"The game {game.GameType} needs `ColorFields` set") { HResult = 4200 };
            }
            VerifyFieldsSet(game.Holes, fields.Count());

            ShapeAndColorResult result = game.AddMove(fields);
            return new SetMoveResponse(game.GameId, game.GameType, game.Moves.Last().MoveNumber, ShapeResult: result);
        }

        Game game = await _gamesRepository.GetGameAsync(moveRequest.GameId) ?? throw new GameNotFoundException();

        SetMoveResponse response = game switch
        {
            Game<ColorField, ColorResult> colorGame => SetColorMove(colorGame, moveRequest.ColorFields),
            Game<ColorField, SimpleColorResult> simpleGame => SetSimpleMove(simpleGame, moveRequest.ColorFields),
            Game<ShapeAndColorField, ShapeAndColorResult> shapeGame => SetShapeMove(shapeGame, moveRequest.ShapeAndColorFields),
            _ => throw new InvalidGameException("game type not found")
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
