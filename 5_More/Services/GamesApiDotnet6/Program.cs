using System.Text.Json.Serialization;

using Codebreaker.Utilities;

using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// two different JsonOptions, swagger description uses Microsoft.AspNetCore.Mvc.Json
builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
});

// typed results use Microsoft.AspNetCore.Http.Json
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
});

builder.Services.AddSingleton<IGamesRepository, InMemoryGamesRepository>();
builder.Services.AddSingleton<GamesFactory>();
builder.Services.AddTransient<IGamesService, GamesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/games", async (IGamesService gamesService) =>
{
    IEnumerable<Game> games = await gamesService.GetGamesAsync();
    return Results.Ok(games);
})
.WithName("GetGames")
.Produces<IEnumerable<Game>>(StatusCodes.Status200OK)
.WithTags("Info");

// Get game by id
app.MapGet("/games/{gameId:guid}", async (Guid gameId, IGamesService gameService) =>
{
    Game? game = await gameService.GetGameAsync(gameId);

    if (game is null)
        return Results.NotFound();

    return Results.Ok(game);
})
.WithName("GetGame")
.Produces<Game>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound)
.WithTags("Info");

// Start a game - create a game object
app.MapPost("/games", async (CreateGameRequest request, IGamesService gamesService) =>
{
    Game? game = null;
    try
    {
        game = await gamesService.CreateGameAsync(request.GameType, request.PlayerName);
    }
    catch (GameException ex) when (ex.HResult == 4000)
    {
        app.Logger.LogError("Game Type not found {gametype}", request.GameType);

        return Results.BadRequest();
    }

    CreateGameResponse createGameResponse = new(game.GameId, game.GameType, game.PlayerName, game.Holes, game.MaxMoves);
    return Results.Created($"/{game.GameId}", createGameResponse);
})
.WithName("CreateGame")
.Produces<CreateGameResponse>(StatusCodes.Status201Created)
.Produces(StatusCodes.Status400BadRequest)
.WithTags("Play");

// Create a move for a game
app.MapPost("/games/{gameId:guid}/moves", async (Guid gameId, SetMoveRequest request, IGamesService gamesService) =>
{
    if (gameId != request.GameId)
    {
        return Results.BadRequest();
    }

    try
    {
        SetMoveResponse response = await gamesService.SetMoveAsync(request);
        return Results.Ok(response);
    }
    catch (GameException ex) when (ex.HResult is > 4200 and < 4300)
    {
        return Results.BadRequest();
    }
    catch (GameException ex) when (ex.HResult == 4400)
    {
        return Results.NotFound();
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "Unexpected error");
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
    }
})
.WithName("SetMove")
.Produces<SetMoveResponse>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status400BadRequest)
.Produces(StatusCodes.Status404NotFound)
.WithTags("Play");

app.Run();
