using System;

public readonly struct Dimensions
{
    public double Length { get; }
    public double Width { get; }

    // With C# 10 you can create a custom parameterless constructor for structs
    public Dimensions()
    {
        Length = 0;
        Width = 0;
    }

    public Dimensions(double length, double width)
    {
        Length = length;
        Width = width;
    }

    public double Diagonal => Math.Sqrt(Length * Length + Width * Width);
    public override string ToString() => $"Length: {Length}, Width: {Width}";
}