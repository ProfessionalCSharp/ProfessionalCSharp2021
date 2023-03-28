using Codebreaker.Models;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddDbContextFactory<GamesContext>(options 
    => options.UseSqlServer(builder.Configuration.GetConnectionString("GamesConnection")));

using IHost app = builder.Build();

var gameId1 = Guid.NewGuid();
var gameId2 = Guid.NewGuid();



var contextFactory = app.Services.GetRequiredService<IDbContextFactory<GamesContext>>();

{
    // create two different games
    using var context = await contextFactory.CreateDbContextAsync();

    await context.Database.EnsureCreatedAsync();

    GameData gameData1 = new(gameId1, "6x4", "player1");
    GameData gameData2 = new(gameId2, "5x5x4", "player2");

    context.Games.Add(gameData1);
    context.Games.Add(gameData2);
    await context.SaveChangesAsync();
}

{
    // add moves to the games
    using var context = await contextFactory.CreateDbContextAsync();

    MoveData<ColorField> moveData1 = new(gameId1, Guid.NewGuid(), 1)
    {
        Fields = new List<ColorField>()
        {
            new("red"),
            new("blue"),
            new("green"),
            new("yellow")
        }
    };

    MoveData<ShapeAndColorField> moveData2 = new(gameId2, Guid.NewGuid(), 1)
    {   
        Fields = new List<ShapeAndColorField>()
        {
            new("circle", "red"),
            new("square", "blue"),
            new("triangle", "green"),
            new("circle", "yellow"),
        }
    };

    GameData game1 = await context.Games.SingleAsync(g => g.GameId == gameId1);
    game1.Moves.Add(moveData1);

    GameData game2 = await context.Games.SingleAsync(g => g.GameId == gameId2);
    game2.Moves.Add(moveData2);

    await context.SaveChangesAsync();   
}

{
    // read the games and moves
    using var context = await contextFactory.CreateDbContextAsync();
    var games = await context.Games
        .Include(g => g.Moves).ToListAsync();

    foreach (var game in games )
    {
        Console.WriteLine(game.GameType);
        foreach (var move in game.Moves)
        {
            Console.WriteLine($"move type: {TypeInformation(move)}");       
        }
    }
}

static string TypeInformation(MoveData moveData) => moveData switch
{
    MoveData<ColorField> => "MoveData<ColorField>",
    MoveData<ShapeAndColorField> => "MoveData<ShapeAndColorField>",
    _ => "unknown"
};
