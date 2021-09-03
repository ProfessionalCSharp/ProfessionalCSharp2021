public record Rectangle(Position Position, Size Size) : Shape(Position, Size)
{
    protected override void DisplayShape()
    {
        Console.WriteLine($"Rectangle at position {Position} with size {Size}");
    }
}

public record Ellipse(Position Position, Size Size) : Shape(Position, Size)
{
    protected override void DisplayShape()
    {
        Console.WriteLine($"Ellipse at position {Position} with size {Size}");
    }
}