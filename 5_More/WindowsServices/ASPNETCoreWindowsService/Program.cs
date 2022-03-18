using Microsoft.Extensions.Logging.EventLog;

WebApplicationOptions options = new()
{
    ContentRootPath = AppContext.BaseDirectory,
    Args = args
};
var builder = WebApplication.CreateBuilder(options);

builder.Host.UseWindowsService();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<EventLogSettings>(config =>
{
    config.LogName = string.Empty;
    config.SourceName = "ASPNETCoreWindowsService";
});


//builder.WebHost.ConfigureKestrel(options =>
//{
// Kestrel is configured with appsettings.json
//});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/", (ILogger<Program> logger) => {
    logger.LogInformation("/ requested");
    return Results.Redirect("/weatherforecast");
});

app.MapGet("/weatherforecast", (ILogger<Program> logger) =>
{
    logger.LogInformation("weatherforecast requested");

    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}