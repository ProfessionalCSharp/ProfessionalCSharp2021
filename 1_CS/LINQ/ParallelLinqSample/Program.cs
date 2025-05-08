﻿using System.Collections.Concurrent;
using System.Diagnostics;

namespace ParallelLinqSample;

class Program
{
    static void Main()
    {
        IList<int> data = SampleData();
        LinqQuery(data);
        ExtensionMethods(data);
        UseAPartitioner(data);
        UseCancellation(data);
    }

    static void LinqQuery(IEnumerable<int> data)
    {
        Console.WriteLine(nameof(LinqQuery));
        var res = (from x in data.AsParallel()
                   where Math.Log(x) < 4
                   select x).Average();
        Console.WriteLine($"result from {nameof(LinqQuery)}: {res}");
        Console.WriteLine();
    }

    static void ExtensionMethods(IEnumerable<int> data)
    {
        Console.WriteLine(nameof(ExtensionMethods));

        Stopwatch stopwatch = new();
        stopwatch.Start();

        var res = data.AsParallel()
            .Where(x => Math.Log(x) < 4)
            .Select(x => x).Average();

        stopwatch.Stop();

        Console.WriteLine($"result from {nameof(ExtensionMethods)}: {res} with elapsed milliseconds {stopwatch.ElapsedMilliseconds}");

        Console.WriteLine();
    }

    static void UseAPartitioner(IList<int> data)
    {
        Console.WriteLine(nameof(UseAPartitioner));
        var res = (from x in Partitioner.Create(data, loadBalance: true).AsParallel()
                   where Math.Log(x) < 4
                   select x).Average();

        Console.WriteLine($"result from {nameof(UseAPartitioner)}: {res}");
        Console.WriteLine();
    }

    static void UseCancellation(IEnumerable<int> data)
    {
        Console.WriteLine(nameof(UseCancellation));
        var cts = new CancellationTokenSource();

        Task.Run(() =>
        {
            try
            {
                var res = (from x in data.AsParallel().WithCancellation(cts.Token)
                           where Math.Log(x) < 4
                           select x).Average();

                Console.WriteLine($"Query finished, sum: {res}");
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
            }
        });

        Console.WriteLine("Query started");
        Console.Write("Cancel? ");
        string? input = Console.ReadLine();
        if (input?.ToLower().Equals("y") == true)
        {
            cts.Cancel();
        }

        Console.WriteLine();
    }

    static IList<int> SampleData()
    {
        const int arraySize = 50_000_000;
        return [.. Enumerable.Range(0, arraySize).Select(x => Random.Shared.Next(140))];
    }
}
