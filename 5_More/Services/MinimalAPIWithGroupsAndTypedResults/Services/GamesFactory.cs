using Codebreaker.Utilities;


namespace Codebreaker.Services;

public class GamesFactory
{
    static readonly private string[] s_colors6 = { "red", "green", "blue", "yellow", "purple", "orange" };
    static readonly private string[] s_colors8 = { "red", "green", "blue", "yellow", "purple", "orange", "pink", "brown" };
    static readonly private string[] s_colors5 = { "red", "green", "blue", "yellow", "purple" };
    static readonly private string[] s_shapes5 = { "circle", "square", "triangle", "star", "rectangle" };

    public Game CreateGame(GameType gameType, string playerName)
    {
        static string GetRandomValue(string[] items) => items[Random.Shared.Next(items.Length)];

        static Game<ColorField, SimpleColorResult> Get6x4SimpleGame(GameType gameType, string playerName) =>
            new(Guid.NewGuid(), gameType, playerName, 4, 12)
            {
                Codes = Enumerable.Range(0, 4).Select(i => new ColorField(GetRandomValue(s_colors6))).ToArray()
            };

        static Game<ColorField, ColorResult> Get6x4Game(GameType gameType, string playerName) =>
            new(Guid.NewGuid(), gameType, playerName, 4, 12)
            {
                Codes = Enumerable.Range(0, 4).Select(i => new ColorField(GetRandomValue(s_colors6))).ToArray()
            };

        static Game<ColorField, ColorResult> Get8x5Game(GameType gameType, string playerName) =>
            new(Guid.NewGuid(), gameType, playerName, 5, 14)
            {
                Codes = Enumerable.Range(0, 5).Select(i => new ColorField(GetRandomValue(s_colors8))).ToArray()
            };

        static Game<ShapeAndColorField, ShapeAndColorResult> Get5x5x4Game(GameType gameType, string playerName) =>
            new(Guid.NewGuid(), gameType, playerName, 4, 14)
            {
                Codes = Enumerable.Range(0, 4).Select(i => new ShapeAndColorField(GetRandomValue(s_shapes5), GetRandomValue(s_colors5))).ToArray()
            };

        return gameType switch
        {
            GameType.Game6x4Simple => Get6x4SimpleGame(gameType, playerName),
            GameType.Game6x4 => Get6x4Game(gameType, playerName),
            GameType.Game8x5 => Get8x5Game(gameType, playerName),
            GameType.Game5x5x4 => Get5x5x4Game(gameType, playerName),
            _ => throw new InvalidGameException("Invalid game type") { HResult = 4000 }
        };
    }
}
