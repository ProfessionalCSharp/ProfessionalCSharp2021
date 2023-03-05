using Codebreaker.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSqlServer<GamesContext>(builder.Configuration.GetConnectionString("GamesConnection") ?? throw new InvalidOperationException());

var app = builder.Build();

var gameId1 = Guid.NewGuid();
var gameId2 = Guid.NewGuid();

{
    // create two different games
    using var gameScope = app.Services.CreateScope();
    var context = gameScope.ServiceProvider.GetRequiredService<GamesContext>();

    await context.Database.EnsureCreatedAsync();

    GameData gameData1 = new(gameId1, "6x4", "player1");
    GameData gameData2 = new GameData(gameId2, "5x5x4", "player2");

    context.Games.Add(gameData1);
    context.Games.Add(gameData2);
    await context.SaveChangesAsync();
}

{
    // add moves to the games
    using var moveScope = app.Services.CreateScope();
    var context = moveScope.ServiceProvider.GetRequiredService<GamesContext>();

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

    var game1 = await context.Games.SingleAsync(g => g.GameId == gameId1);
    game1.Moves.Add(moveData1);

    var game2 = await context.Games.SingleAsync(g => g.GameId == gameId2);
    game2.Moves.Add(moveData2);

    await context.SaveChangesAsync();   
}

{
    // read the games and moves
    using var gamesscope = app.Services.CreateScope();
    var context = gamesscope.ServiceProvider.GetRequiredService<GamesContext>();
    var games = await context.Games
        .Include(g => g.Moves).ToListAsync();
    foreach ( var game in games )
    {
        Console.WriteLine(game.GameType);
        foreach (var move in game.Moves)
        {
            Console.WriteLine($"move type: {TypeInforation(move)}");       
        }
    }
}

string TypeInforation(MoveData moveData)
{
    return moveData switch
    {
        MoveData<ColorField> => "MoveData<ColorField>",
        MoveData<ShapeAndColorField> => "MoveData<ShapeAndColorField>",
        _ => "unknown"
    };
}
