Rectangle r1 = new(new Position(33, 22), new Size(200, 100));
Rectangle r2 = r1 with { Position = new Position(100, 22) };
Ellipse e1 = new(new Position(122, 200), new Size(40, 20));

DisplayShapes(r1, r2, e1);

void DisplayShapes(params Shape[] shapes)
{
    foreach (var shape in shapes)
    {
        shape.Draw();
    }
}