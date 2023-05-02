using Codebreaker.Utilities;

namespace Codebreaker.Endpoints;

public class GameMoveExceptionFilter : IEndpointFilter
{
    private readonly ILogger _logger;
    public GameMoveExceptionFilter(ILogger<GameMoveExceptionFilter> logger)
    {
        _logger = logger;
    }
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        SetMoveRequest request = context.GetArgument<SetMoveRequest>(1);
        try
        {
            return await next(context);
        }
        catch (GameNotFoundException ex)
        {
            _logger.LogError(ex, "Game {gameid} not found", request.GameId);
            return TypedResults.NotFound();
        }
    }
}