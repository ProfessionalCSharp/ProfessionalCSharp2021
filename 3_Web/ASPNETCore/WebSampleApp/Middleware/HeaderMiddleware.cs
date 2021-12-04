namespace WebSampleApp.Middleware;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class HeaderMiddleware
{
    private readonly RequestDelegate _next;

    public HeaderMiddleware(RequestDelegate next) => _next = next;

    public Task Invoke(HttpContext httpContext)
    {
        httpContext.Response.Headers.Add("CustomHeader2", "custom header value");
        return _next(httpContext);
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class HeaderMiddlewareExtensions
{
    public static IApplicationBuilder UseHeaderMiddleware(this IApplicationBuilder builder)
        => builder.UseMiddleware<HeaderMiddleware>();
}
