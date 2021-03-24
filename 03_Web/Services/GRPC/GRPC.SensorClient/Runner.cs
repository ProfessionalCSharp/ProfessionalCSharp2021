using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GRPCService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

    public async Task Run()
    {
        CancellationTokenSource cts = new(5000); // cancel after 5 seconds

        try
        {

            using var stream = _sensorClient.GetSensorData(new Empty(), cancellationToken: cts.Token);

            await foreach (var data in stream.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine($"data {data.Val1} {data.Val2} {data.TimeStamp.ToDateTime():T}");
            }
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogInformation(ex.Message);
        }
    }
}
