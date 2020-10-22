using Microsoft.Extensions.Logging;

class Program
{
    static void Main()
    {
        using var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole().AddDebug();
        });

        ILogger<Program> logger = loggerFactory.CreateLogger<Program>();
        logger.LogInformation("Info Message");
    }
}
