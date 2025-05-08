public class Rectangle(ILogger logger) : Shape(logger)
{
    protected override void DisplayShape()
    {
        Logger.Log($"Rectangle at position {Position} with size {Size}");
    }
}

public class Ellipse(ILogger logger) : Shape(logger)
{
    protected override void DisplayShape()
    {
        Logger.Log($"Ellipse at position {Position} with size {Size}");
    }
}