CancellationTokenSource cancellation = new(TimeSpan.FromSeconds(5));

try
{
    await RunTaskAsync(cancellation.Token);
}
catch (OperationCanceledException ex)
{
    Console.WriteLine(ex.Message);
}

Task RunTaskAsync(CancellationToken cancellationToken) =>
    Task.Run(async () =>
    {
        while (true)
        {
            Console.Write(".");
            await Task.Delay(100);
            if (cancellationToken.IsCancellationRequested)
            {
                // do some cleanup
                Console.WriteLine("resource cleanup and good bye!");
                cancellationToken.ThrowIfCancellationRequested();
            }
        }
    });
