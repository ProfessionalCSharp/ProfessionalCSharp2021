using System;
using System.Threading;
using System.Threading.Tasks;

CancelParallelFor();
CancelTask();
Console.ReadLine();

void CancelTask()
{
    Console.WriteLine(nameof(CancelTask));
    CancellationTokenSource cts = new(millisecondsDelay: 500);
    cts.Token.Register(() => Console.WriteLine("*** task canceled"));

    Task t1 = Task.Run(() =>
    {
        Console.WriteLine("in task");
        for (int i = 0; i < 20; i++)
        {
            Task.Delay(100).Wait();
            CancellationToken token = cts.Token;
            if (token.IsCancellationRequested)
            {
                Console.WriteLine("cancelling was requested, " +
                  "cancelling from within the task");
                token.ThrowIfCancellationRequested();
                break;
            }
            Console.WriteLine("in loop");
        }
        Console.WriteLine("task finished without cancellation");
    }, cts.Token);
    try
    {
        t1.Wait();
    }
    catch (AggregateException ex)
    {
        Console.WriteLine($"exception: {ex.GetType().Name}, {ex.Message}");
        foreach (var innerException in ex.InnerExceptions)
        {
            Console.WriteLine($"inner exception: {innerException.GetType()}," +
              $"{innerException.Message}");
        }
    }
    Console.WriteLine();
}

void CancelParallelFor()
{
    Console.WriteLine(nameof(CancelParallelFor));
    CancellationTokenSource cts = new(millisecondsDelay: 500);
    cts.Token.Register(() => Console.WriteLine("*** cancellation activated"));

    try
    {
        ParallelLoopResult result =
          Parallel.For(0, 100, new ParallelOptions
          {
              CancellationToken = cts.Token,
          },
          x =>
          {
              Console.WriteLine($"loop {x} started");
              int sum = 0;
              for (int i = 0; i < 100; i++)
              {
                  Task.Delay(2).Wait();
                  sum += i;
              }
              Console.WriteLine($"loop {x} finished");
          });
    }
    catch (OperationCanceledException ex)
    {
        Console.WriteLine(ex.Message);
    }
    Console.WriteLine();
}
