namespace Codebreaker.Models;

public record ShapeAndColorField(string Shape, string Color) : IParsable<ShapeAndColorField>
{
    public override string ToString() => $"{Shape};{Color}";

    public static ShapeAndColorField Parse(string s, IFormatProvider? provider)
    {
        if (TryParse(s, provider, out ShapeAndColorField? shape))
        {
            return shape;
        }
        else
        {
            throw new ArgumentException($"Cannot parse value {s}", nameof(s));
        }
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out ShapeAndColorField result)
    {
        result = null;
        if (s is null)
        {
            return false;
        }
        string[] parts = s.Split(';');
        if (parts.Length != 2)
        {
            return false;
        }
        result = new ShapeAndColorField(parts[0], parts[1]);
        return true;
    }
}
