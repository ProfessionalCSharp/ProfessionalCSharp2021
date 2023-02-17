namespace DISample;

public class GreetingService : IGreetingService
{
    public string Greet(string name) => $"Hello, {name}";
}
