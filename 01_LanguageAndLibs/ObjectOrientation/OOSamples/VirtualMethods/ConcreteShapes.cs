using System;

namespace VirtualMethods
{
    public record Rectangle : Shape
    {
        public override void Draw() =>
            Console.WriteLine($"Rectangle with {Position} and {Size}");

        public override void Move(Position newPosition)
        {
            Console.Write("Rectangle ");
            base.Move(newPosition);
        }

        public override void Resize(int width, int height)
        {
            throw new NotImplementedException();
        }
    }

    public record Ellipse : Shape
    {
        public override void Resize(int width, int height)
        {
            if (Size != null)
            {
                Size.Width = width;
                Size.Height = height;
            }
        }
    }
}