using HttpServerSample;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(kestrelOptions =>
{
    kestrelOptions.AddServerHeader = true;
    kestrelOptions.AllowResponseHeaderCompression = true;
    kestrelOptions.Limits.Http2.MaxStreamsPerConnection = 10;
    kestrelOptions.Limits.MaxConcurrentConnections = 20;
    kestrelOptions.ConfigureHttpsDefaults(httpsConfig =>
    {

    });
}).UseUrls("http://localhost:5020", "https://localhost:5021");


builder.Services.AddScoped<GenerateHtml>();
builder.Services.AddSingleton<Formula1>();

var app = builder.Build();

app.MapGet("api/racers", async (Formula1 formula1, HttpContext context) =>
{
    await context.Response.WriteAsJsonAsync(formula1.GetChampions());
});

app.MapGet("api/racersdelay", async (Formula1 formula1, HttpContext context) =>
{
    await Task.Delay(3000);
    await context.Response.WriteAsJsonAsync(formula1.GetChampions());
});

app.MapGet("/", async(GenerateHtml generateHtml, HttpContext context) =>
{
    string content = generateHtml.GetHtmlContent(context.Request);
    context.Response.ContentType = "text/html";
    await context.Response.WriteAsync(content);
});

app.Run();
