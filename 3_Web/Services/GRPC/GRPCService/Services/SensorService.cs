using Google.Protobuf.WellKnownTypes;

using Grpc.Core;

namespace GRPCService.Services;

public class SensorService : Sensor.SensorBase
{
    private readonly ILogger _logger;
    public SensorService(ILogger<SensorService> logger)
    {
        _logger = logger;
    }

    public override async Task GetSensorData(Empty request, IServerStreamWriter<SensorData> responseStream, ServerCallContext context)
    {
        try
        {
            while (!context.CancellationToken.IsCancellationRequested)
            {
                await Task.Delay(100, context.CancellationToken);
                SensorData data = new()
                {
                    Timestamp = Timestamp.FromDateTime(DateTime.UtcNow),
                    Val1 = Random.Shared.Next(100),
                    Val2 = Random.Shared.Next(100)
                };
                Console.WriteLine($"returning data {data}");
                await responseStream.WriteAsync(data);
            }
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogInformation(ex.Message);
        }
    }
}
