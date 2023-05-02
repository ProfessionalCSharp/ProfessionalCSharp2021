using System.Collections.Concurrent;

namespace Codebreaker.Services;

public class InMemoryGamesRepository : IGamesRepository
{
    private readonly ConcurrentDictionary<Guid, Game> _games = new();

    public Task<Game> AddGameAsync(Game game)
    {
        _games.AddOrUpdate(game.GameId, game, (id, game) => game);
        return Task.FromResult(game);
    }

    public Task<Game?> GetGameAsync(Guid gameId)
    {
        if (_games.TryGetValue(gameId, out var game))
        {
            return Task.FromResult<Game?>(game);
        }
        else
        {
            return Task.FromResult<Game?>(default);
        }
    }

    public Task<IEnumerable<Game>> GetGamesAsync()
    {
        return Task.FromResult(_games.Values.AsEnumerable());
    }

    public Task<Game> UpdateGameAsync(Guid gameId, Game game)
    {
        return Task.FromResult(game);
    }
}
