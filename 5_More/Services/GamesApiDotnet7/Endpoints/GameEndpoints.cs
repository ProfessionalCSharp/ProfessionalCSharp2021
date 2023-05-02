using Codebreaker.Utilities;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Codebreaker.Endpoints;

public static class GameEndpoints
{
    public static IEndpointRouteBuilder MapGameEndpoints(this IEndpointRouteBuilder routes, ILogger logger)
    {
        var group = routes.MapGroup("/games")
            .WithTags("Gameplay")
            .AddEndpointFilter<LoggingFilter>();

        group.MapGet("/", async (IGamesService gamesService) =>
        {
            IEnumerable<Game> games = await gamesService.GetGamesAsync();
            return TypedResults.Ok(games);
        })
        .WithName("GetGames")
        .WithSummary("Get all games");

        // Get game by id
        group.MapGet("/{gameId:guid}", async Task<Results<Ok<Game>, NotFound>> (Guid gameId, IGamesService gameService) =>
        {
            Game? game = await gameService.GetGameAsync(gameId);

            if (game is null)
                return TypedResults.NotFound();

            return TypedResults.Ok(game);
        })
        .WithName("GetGame")
        .WithSummary("Gets a game by the given id")
        .WithOpenApi(op =>
        {
            op.Parameters[0].Description = "The game-id to request a game run";
            return op;
        });

        // Start a game - create a game object
        group.MapPost("/", async Task<Results<Created<CreateGameResponse>, BadRequest>> (CreateGameRequest request, IGamesService gamesService) =>
        {
            Game? game = null;
            try
            {
                game = await gamesService.CreateGameAsync(request.GameType, request.PlayerName);

                CreateGameResponse createGameResponse = new(game.GameId, game.GameType, game.PlayerName);
                return TypedResults.Created($"/{game.GameId}", createGameResponse);
            }
            catch (InvalidGameException ex) when (ex.HResult == 4000)
            {
                logger.LogError("Game Type not found {gametype}", request.GameType);
                return TypedResults.BadRequest();
            }
        })
        .WithName("CreateGame")
        .WithSummary("Creates and starts a game")
        .WithOpenApi(op =>
        {
            op.RequestBody.Description = "The data of the game to create";
            return op;
        });

        // Create a move for a game
        group.MapPost("/{gameId:guid}/moves", async Task<Results<Ok<SetMoveResponse>, BadRequest<string>, NotFound>> (Guid gameId, SetMoveRequest request, IGamesService gamesService) =>
        {
            SetMoveResponse response = await gamesService.SetMoveAsync(request);
            return TypedResults.Ok(response);
        })
        .AddEndpointFilter<GameMoveValidationFilter>()
        .AddEndpointFilter<GameMoveExceptionFilter>()
        .WithName("SetMove")
        .WithSummary("Sets a move with a game")
        .WithOpenApi(op =>
        {
            op.RequestBody.Description = "The game move consisting of values for every available hole";
            return op;
        });

        return routes;
    }
}
