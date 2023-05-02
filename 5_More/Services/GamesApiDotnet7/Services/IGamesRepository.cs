namespace Codebreaker.Services;

public interface IGamesRepository
{
    Task<Game?> GetGameAsync(Guid gameId);
    Task<IEnumerable<Game>> GetGamesAsync();
    Task<Game> AddGameAsync(Game game);
    // update game including moves
    Task<Game> UpdateGameAsync(Guid gameId, Game game);
}
