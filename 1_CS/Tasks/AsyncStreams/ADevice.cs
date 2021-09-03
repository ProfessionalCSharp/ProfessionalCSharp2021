public record SensorData(int Value1, int Value2);

public class ADevice
{
    private Random _random = new();
    public async IAsyncEnumerable<SensorData> GetSensorData([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        while(true)
        {
            await Task.Delay(250, cancellationToken);
            yield return new SensorData(_random.Next(20), _random.Next(20));
        }
    }
}
