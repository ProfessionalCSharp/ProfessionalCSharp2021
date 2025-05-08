public class Rectangle(int x, int y, int width, int height) : Shape(x, y, width, height)
{
    protected override void DisplayShape()
    {
        Console.WriteLine($"Rectangle at position {Position} with size {Size}");
    }

    public override Rectangle Clone() => new(Position.X, Position.Y, Size.Width, Size.Height);
}

public class Ellipse(int x, int y, int width, int height) : Shape(x, y, width, height)
{
    protected override void DisplayShape()
    {
        Console.WriteLine($"Ellipse at position {Position} with size {Size}");
    }

    public override Ellipse Clone() => new(Position.X, Position.Y, Size.Width, Size.Height);
}