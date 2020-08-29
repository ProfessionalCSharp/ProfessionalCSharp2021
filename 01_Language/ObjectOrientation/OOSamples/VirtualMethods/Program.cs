using VirtualMethods;

var r = new Rectangle { Position = new Position { X = 33, Y = 22 }, Size = new Size { Width = 200, Height = 100 } };
r.Draw();
DrawShape(r);

r.Move(new Position { X = 120, Y = 40 });
r.Draw();

Shape s1 = new Ellipse();
DrawShape(s1);

static void DrawShape(Shape shape) => shape.Draw();
