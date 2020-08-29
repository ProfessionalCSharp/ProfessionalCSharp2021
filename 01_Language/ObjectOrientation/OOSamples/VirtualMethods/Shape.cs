using static System.Console;

namespace VirtualMethods
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public override string ToString() => $"X: {X}, Y: {Y}";
    }

    public class Size
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public override string ToString() => $"Width: {Width}, Height: {Height}";
    }

    public abstract record Shape
    {
        public Position? Position { get; init; }
        public Size? Size { get; init; }
        public virtual void Draw() => WriteLine($"Shape with {Position} and {Size}");

        public virtual void Move(Position newPosition)
        {
            if (Position != null)
            {
                Position.X = newPosition.X;
                Position.Y = newPosition.Y;
                WriteLine($"moves to {Position}");
            }
        }

        public abstract void Resize(int width, int height);
    }

}