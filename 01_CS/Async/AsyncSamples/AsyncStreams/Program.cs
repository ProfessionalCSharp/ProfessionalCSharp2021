using System;
using System.Threading;
using System.Threading.Tasks;

CancellationTokenSource cancellation = new(TimeSpan.FromSeconds(5));

ADevice aDevice = new();
try
{
    await foreach (var data in aDevice.GetSensorData().WithCancellation(cancellation.Token))
    {
        Console.WriteLine($"{data.Value1} {data.Value2}");
    }
}
catch (OperationCanceledException ex)
{
    Console.WriteLine(ex.Message);
}