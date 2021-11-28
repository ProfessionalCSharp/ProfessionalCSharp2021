Dimensions point = new(3, 6);
Console.WriteLine(point);

// deconstruct
(double length, _) = point;
Console.WriteLine(length);
Dimensions point2 = new();
Dimensions point3 = point2 with { Length = 3, Width = 6 };

if (point3 == point)
{
    Console.WriteLine("the same values");
}

Console.ReadLine();

public record struct Dimensions(double Length, double Width)
{
    public double Diagonal => Math.Sqrt(Length * Length + Width * Width);
}
