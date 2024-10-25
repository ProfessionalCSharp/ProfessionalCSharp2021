namespace SimpleWorkerService;

public class Worker(ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        string[] args = Environment.GetCommandLineArgs();
        logger.LogInformation("command line args: {args}", string.Join(" ", args));

        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(10000, stoppingToken);
        }
    }
}
