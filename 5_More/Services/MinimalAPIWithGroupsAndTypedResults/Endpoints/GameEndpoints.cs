using Codebreaker.Utilities;

using Microsoft.AspNetCore.Http.HttpResults;

namespace Codebreaker.Endpoints;

// polymorphic hierarchy https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/polymorphism?pivots=dotnet-7-0

public static class GameEndpoints
{
    public static IEndpointRouteBuilder MapGameEndpoints(this IEndpointRouteBuilder routes, ILogger logger)
    {
        var group = routes.MapGroup("/games")
            .WithTags(nameof(Game));

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
            op.Parameters[0].Description = "The id of the game to get";
            return op;
        });

        // Start a game - create a game object
        group.MapPost("/", async Task<Results<Created<CreateGameResponse>, BadRequest<InvalidGameRequest>>> (CreateGameRequest request, IGamesService gamesService) =>
        {
            Game? game = null;
            try
            {
                game = await gamesService.CreateGameAsync(request.GameType, request.PlayerName);
            }
            catch (InvalidGameException ex) when (ex.HResult == 4000)
            {
                logger.LogError("Game Type not found {gametype}", request.GameType);

                InvalidGameRequest invalidRequest = new("Gametype does not exist, valid types:", new[] { GameTypes.Game6x4, GameTypes.Game8x5, GameTypes.Game5x5x4, GameTypes.Game6x4Simple });
                return TypedResults.BadRequest(invalidRequest);
            }

            CreateGameResponse createGameResponse = new(game.GameId, game.GameType, game.PlayerName);
            return TypedResults.Created($"/{game.GameId}", createGameResponse);
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
            if (gameId != request.GameId)
            {
                return TypedResults.BadRequest("id does not match");
            }
            if (request.ColorFields == default && request.ShapeAndColorFields == default)
            {
                return TypedResults.BadRequest("Either fill ColorFields or ShapeAndColorFields");
            }
            try
            {
                SetMoveResponse response = await gamesService.SetMoveAsync(request);
                return TypedResults.Ok(response);
            }
            catch (GameNotFoundException)
            {
                logger.LogError("Game {gameid} not found", request.GameId);
                return TypedResults.NotFound();
            }
        })
        .WithName("SetMove")
        .WithSummary("Sets a move with a game")
        .WithOpenApi(op =>
        {
            op.RequestBody.Description = "The move to set";
            return op;
        });

        return routes;
    }
}
