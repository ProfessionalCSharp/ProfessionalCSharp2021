using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Book b1 = new("Professional C# and .NET", "Wrox Press");
Console.WriteLine(b1.Title);  // property with get and init accessors

ColorResult gameResult = new(2, 1);
Console.WriteLine(gameResult.FullMatch);  // property with get and init accessors

ShapeAndColorResult gameResult2 = new(2, 1, 1);
Console.WriteLine(gameResult2.FullMatch);  // property with get and set accessors

var builder = Host.CreateApplicationBuilder();
builder.Services.AddTransient<IGreetingService, GreetingService>();
builder.Services.AddTransient<HomeController>();
using var app = builder.Build();
var controller = app.Services.GetRequiredService<HomeController>();
controller.Index("Katharina");

// primary constructor with properties

Person p1 = new("Bruce", "Wayne")
{
    FirstName = "Thomas"
};
Console.WriteLine(p1);

// primary constructor with inheritance

Rectangle r1 = new(new(20, 20), new(20, 20));
Console.WriteLine($"{r1.Position} {r1.Size}");

// multiple constructors

Game<ColorField, ColorResult> g1 = new("clark", "6x4");
Console.WriteLine($"{g1.PlayerName} {g1.GameType}");

public record class Book(string Title, string Publisher);

public record struct ColorResult(int FullMatch, int ColorMatch);

public readonly record struct ShapeAndColorResult(
    int FullMatch, 
    int ColorAndShapeMatch, 
    int ColorOrShapeMatch);

public interface IGreetingService
{
    string Hello(string name);
}

public class Person(string firstName, string lastName)
{
    public string LastName => lastName;
    public string FirstName {
        get => firstName;
        set => firstName = value;
    }

    public override string ToString() => $"{FirstName} {LastName}";
}

public class GreetingService() : IGreetingService
{
    public string Hello(string name) => $"Hello, {name}";
}

public class HomeController(IGreetingService greetingService)
{
    public void Index(string name)
    {
        string result = greetingService.Hello(name);
        Console.WriteLine(result);
        greetingService = new GreetingService();
    }
}

public record struct Position(double X, double Y);
public record struct Size(double Width, double Height);

public abstract class Shape(Position position, Size size)
{
    public Position Position => position;
    public Size Size => size;     
}

public class Rectangle(Position position, Size size)
    : Shape(position, size)
{ }

public class Ellipse(Position position, Size size)
    : Shape(position, size)
{ }

public record struct ColorField(string Color);

public class Game<TField, TResult>(string playerName, string gameType, TField[] codes, TResult[] results)
{
    public Game(string playerName, string gameType)
        : this(playerName, gameType, Array.Empty<TField>(), Array.Empty<TResult>())
    {        
    }

    public string PlayerName => playerName;
    public string GameType => gameType;
    public TField[] Codes => codes;
    public TResult[] Results => results;
}
