public record SensorData(int Value1, int Value2);

public class ADevice
{
    public async IAsyncEnumerable<SensorData> GetSensorData([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        while(true)
        {
            await Task.Delay(250, cancellationToken);
            yield return new SensorData(Random.Shared.Next(20), Random.Shared.Next(20));
        }
    }
}
