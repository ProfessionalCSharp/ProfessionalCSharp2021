Rectangle r1 = new();
r1.Position.X = 33;
r1.Position.Y = 22;
r1.Size.Width = 200;
r1.Size.Height = 100;

Rectangle r2 = r1.Clone();
r2.Position.X = 300;

Ellipse e1 = new();
e1.Position.X = 122;
e1.Position.Y = 200;
e1.Size.Width = 40;
e1.Size.Height = 20;

DisplayShapes(r1, r2, e1);

r1.Move(new Position { X = 120, Y = 40 });

void DisplayShapes(params Shape[] shapes)
{
    foreach (var shape in shapes)
    {
        shape.Draw();
    }
}
