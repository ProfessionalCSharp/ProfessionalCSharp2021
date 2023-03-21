using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

using WebSampleApp;
using WebSampleApp.Extensions;
using WebSampleApp.Middleware;
using WebSampleApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<SessionSample>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(10));

builder.Services.AddScoped<RequestAndResponseSamples>();

builder.Services.AddSingleton<HealthSample>();
builder.Services.AddHealthChecks()
    .AddCheck<CustomHealthCheck>("livecheck", HealthStatus.Unhealthy, tags: new[] { "liveness" })
    .AddCheck<CustomReadyCheck>("readycheck", HealthStatus.Degraded, tags: new[] { "readiness" });

var app = builder.Build();

app.Use((context, next) =>
{
    context.Response.Headers.Add("CustomHeader1", "custom header value");
    return next(context);
});

app.UseStaticFiles();

app.UseSession();

app.UseHeaderMiddleware();

app.UseRouting();

app.MapHealthChecks("/health/allchecks");
app.MapHealthChecks("/health/live", new HealthCheckOptions()
{
    Predicate = reg => reg.Tags.Contains("liveness"),
    ResultStatusCodes = new Dictionary<HealthStatus, int>()
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    },
});
app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = reg => reg.Tags.Contains("readiness"),
    ResponseWriter = async (context, writer) =>
    {
        context.Response.StatusCode = writer.Status switch
        {
            HealthStatus.Healthy => StatusCodes.Status200OK,
            HealthStatus.Degraded => StatusCodes.Status503ServiceUnavailable,
            HealthStatus.Unhealthy => StatusCodes.Status503ServiceUnavailable,
            _ => StatusCodes.Status503ServiceUnavailable
        };

        if (writer.Status == HealthStatus.Healthy)
        {
            await context.Response.WriteAsync("ready");
        }
        else
        {
            await context.Response.WriteAsync(writer.Status.ToString());
            await context.Response.WriteAsync($"duration: {writer.TotalDuration}");
        }
    }
});

app.Map("sethealthy", async context =>
{
    var changeHealthService = context.RequestServices.GetRequiredService<HealthSample>();
    string? healthyValue = context.Request.Query["healthy"];
    if (bool.TryParse(healthyValue, out bool healthy))
    {
        changeHealthService.SetHealthy(healthy);
    }
    else
    {
        await context.Response.WriteAsync("Missing healthy query parameter");
    }

});

app.Map("/randr/{action?}", async context =>
{
    var service = context.RequestServices.GetRequiredService<RequestAndResponseSamples>();
    string? action = context.GetRouteValue("action")?.ToString();
    string method = context.Request.Method;
    string result = (action, method) switch
    {
        (null, "GET") => service.GetRequestInformation(context.Request),
        ("header", "GET") => service.GetHeaderInformation(context.Request),
        ("add", "GET") => service.QueryParameters(context.Request),
        ("content", "GET") => service.Content(context.Request),
        ("form", "GET" or "POST") => service.Form(context.Request),
        ("writecookie", "GET") => service.WriteCookie(context.Response),
        ("readcookie", "GET") => service.ReadCookie(context.Request),
        ("json", "GET") => service.GetJson(context.Response),
        _ => string.Empty
    } ?? throw new InvalidOperationException("result should not be null with the previous operation");

    if (action is "json")
    {
        await context.Response.WriteAsync(result);
    }
    else
    {
        string doc = result.HtmlDocument("Request and Response Samples");
        await context.Response.WriteAsync(doc);
    }
});

app.Map("/add/{x:int}/{y:int}", async context =>
{
    int x = int.Parse(context.GetRouteValue("x")?.ToString() ?? "0");
    int y = int.Parse(context.GetRouteValue("y")?.ToString() ?? "0");
    await context.Response.WriteAsync($"The result of {x} + {y} is {x + y}");
});

app.Map("/session", async context =>
{
    var service = context.RequestServices.GetRequiredService<SessionSample>();
    await service.SessionAsync(context);
});

app.MapGet("/", async context =>
{
    string content = """
            <ul>
              <li><a href="/hello.html">Static Files</a> - requires UseStaticFiles</li>
              <li><a href="/add/37/5">Route Constraints</a></li>
              <li>Request and Response
                <ul>
                  <li><a href="/randr">Request and Response</a></li>
                  <li><a href="/randr/header">Request headers</a></li>
                  <li><a href="/randr/add?x=38&y=4">Add</a></li>
                  <li><a href="/randr/content?data=sample">Content</a></li>
                  <li><a href="/randr/content?data=<h1>Heading 1</h1>">HTML Content</a></li>
                  <li><a href="/randr/content?data=<script>alert('hacker');</script>">Bad Content</a></li>
                  <li><a href="/randr/encoded?data=<h1>sample</h1>">Encoded content</a></li>
                  <li><a href="/randr/encoded?data=<script>alert('hacker');</script>">Encoded bad Content</a></li>
                  <li><a href="/randr/form">Form</a></li>
                  <li><a href="/randr/writecookie">Write cookie</a></li>
                  <li><a href="/randr/readcookie">Read cookie</a></li>
                  <li><a href="/randr/json">JSON</a></li>
                </ul>
              </li>
              <li><a href="/session">Session</a></li>
              <li>Health check
                <ul>
                  <li><a href="/health/live">live</a></li>
                  <li><a href="/health/ready">ready</a></li>
                  <li><a href="/health/allchecks">all checks</a></li>
                  <li><a href="/sethealthy?healthy=true">set healthy</a></li>
                  <li><a href="/sethealthy?healthy=false">set unhealthy</a></li>
                </ul>
              </li>
            </ul>
        """;
    string html = content.HtmlDocument("Web Sample App");

    await context.Response.WriteAsync(html);
});

app.Run();
