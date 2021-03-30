public class Rectangle : Shape
{
    public Rectangle(ILogger logger) : base(logger) {  }

    protected override void DisplayShape()
    {
        Logger.Log($"Rectangle at position {Position} with size {Size}");
    }
}

public class Ellipse : Shape
{
    public Ellipse(ILogger logger) : base(logger) { }

    protected override void DisplayShape()
    {
        Logger.Log($"Ellipse at position {Position} with size {Size}");
    }
}