namespace DISample;

public class GreetingService(IOptions<GreetingServiceOptions> options) : IGreetingService
{
    private string? _from = options.Value.From;

    public string Greet(string name) => $"Hello, {name}! Greetings from {_from}";
}
