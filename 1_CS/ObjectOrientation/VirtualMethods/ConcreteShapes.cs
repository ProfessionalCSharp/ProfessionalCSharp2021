public class Rectangle : Shape
{
    protected override void DisplayShape()
    {
        Console.WriteLine($"Rectangle at position {Position} with size {Size}");
    }

    public override void Move(Position newPosition)
    {
        Console.Write("Rectangle ");
        base.Move(newPosition);
    }

    public override Rectangle Clone()
    {
        Rectangle r = new();
        r.Position.X = Position.X;
        r.Position.Y = Position.Y;
        r.Size.Width = Size.Width;
        r.Size.Height = Size.Height;
        return r;
    }
}

public class Ellipse : Shape
{
    protected override void DisplayShape()
    {
        Console.WriteLine($"Ellipse at position {Position} with size {Size}");
    }

    public override void Move(Position newPosition)
    {
        Console.Write("Ellipse ");
        base.Move(newPosition);
    }

    public override Ellipse Clone()
    {
        Ellipse e = new();
        e.Position.X = Position.X;
        e.Position.Y = Position.Y;
        e.Size.Width = Size.Width;
        e.Size.Height = Size.Height;
        return e;
    }

}
