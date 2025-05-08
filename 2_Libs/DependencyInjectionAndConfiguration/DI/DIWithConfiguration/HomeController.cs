namespace DISample;

public class HomeController(IGreetingService greetingService)
{
    public string Hello(string name) =>
        greetingService.Greet(name);
}
