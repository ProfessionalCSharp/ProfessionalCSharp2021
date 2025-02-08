namespace WebSampleApp.Middleware;

public class HeaderMiddleware(RequestDelegate next)
{
    public Task Invoke(HttpContext httpContext)
    {
        httpContext.Response.Headers.Append("CustomHeader2", "custom header value");
        return next(httpContext);
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class HeaderMiddlewareExtensions
{
    public static IApplicationBuilder UseHeaderMiddleware(this IApplicationBuilder builder)
        => builder.UseMiddleware<HeaderMiddleware>();
}
