using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GRPCService;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

public class Runner
{
    private readonly Sensor.SensorClient _sensorClient;
    private readonly ILogger _logger;
    public Runner(Sensor.SensorClient sensorClient, ILogger<Runner> logger)
    {
        _sensorClient = sensorClient;
        _logger = logger;
    }

    public async Task RunAsync()
    {
        CancellationTokenSource cts = new(10000); // cancel after 10 seconds

        try
        {
            using var stream = _sensorClient.GetSensorData(new Empty());

            await foreach (var data in stream.ResponseStream.ReadAllAsync().WithCancellation(cts.Token))
            {
                Console.WriteLine($"data {data.Val1} {data.Val2} {data.Timestamp.ToDateTime():T}");
            }
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogInformation(ex.Message);
        }
    }
}
