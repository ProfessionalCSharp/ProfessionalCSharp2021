using System.Net.Http.Json;

namespace ServiceDiscoverySample;

public class Runner(HttpClient client)
{
    public async Task ShowWeatherAsync()
    {
        var weatherList = await client.GetFromJsonAsync<IEnumerable<WeatherForecast>>("/weatherforecast") ?? throw new InvalidOperationException();

        foreach (var weather in weatherList)
        {
            Console.WriteLine(weather); 
        }
    }
}

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
