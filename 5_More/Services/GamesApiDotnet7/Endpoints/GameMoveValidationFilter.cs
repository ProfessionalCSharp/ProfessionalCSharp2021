namespace Codebreaker.Endpoints;

public class GameMoveValidationFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        Guid id = context.GetArgument<Guid>(0);
        SetMoveRequest request = context.GetArgument<SetMoveRequest>(1);
        if (id != request.GameId)
        {
            return TypedResults.BadRequest("id does not match");
        }

        if (request.ColorFields == default && request.ShapeAndColorFields == default)
        {
            return TypedResults.BadRequest("Either fill ColorFields or ShapeAndColorFields");
        }
        return await next(context);
    }
}
