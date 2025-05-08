public record Position(int X, int Y);

public record Size(int Width, int Height);

public abstract class Shape(ILogger logger)
{
    protected ILogger Logger { get; } = logger;
    public Position? Position { get; init; }
    public Size? Size { get; init; }

    public void Draw() => DisplayShape();

    protected virtual void DisplayShape()
    {
        Logger.Log($"Shape with {Position} and {Size}");
    }
}
