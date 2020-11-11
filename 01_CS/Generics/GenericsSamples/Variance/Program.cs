using System;
using Wrox.ProCSharp.Generics;

IIndex<Rectangle> rectangles = RectangleCollection.GetRectangles();
IIndex<Shape> shapes = rectangles;

for (int i = 0; i < shapes.Count; i++)
{
    Console.WriteLine(shapes[i]);
}

IDisplay<Shape> shapeDisplay = new ShapeDisplay();
IDisplay<Rectangle> rectangleDisplay = shapeDisplay;
rectangleDisplay.Show(rectangles[0]);