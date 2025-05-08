namespace DISample;

public class HomeController(IGreetingService greetingService)
{
    private readonly IGreetingService _greetingService = greetingService ?? throw new ArgumentNullException(nameof(greetingService));

    public string Hello(string name) => _greetingService.Greet(name);
}
