using System.Diagnostics.CodeAnalysis;

namespace Codebreaker.Models;

public readonly record struct ShapeAndColorResult(byte Correct, byte WrongPosition, byte ColorOrShape) : ISpanParsable<ShapeAndColorResult>, ISpanFormattable
{
    public static ShapeAndColorResult Parse(ReadOnlySpan<char> s, IFormatProvider? provider = default)
    {
        if (TryParse(s, provider, out var result))
        {
            return result;
        }
        else
        {
            throw new FormatException($"Cannot parse {s}");
        }
    }

    public static ShapeAndColorResult Parse(string s, IFormatProvider? provider = default) => Parse(s.AsSpan(), provider);

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out ShapeAndColorResult result)
    {
        result = s switch
        {
            { Length: > 5 or < 5 } => default,
            [ var x, ':', var y, ':', var z ] => new ShapeAndColorResult((byte)(x - '0'), (byte)(y - '0'), (byte)(z - '0')),
            _ => default,
        };

        return result != default;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out ShapeAndColorResult result)
        => TryParse(s.AsSpan(), provider, out result);

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        var destination = new char[5].AsSpan();
        if (TryFormat(destination, out _, format.AsSpan(), formatProvider))
        {
            return destination.ToString();
        }
        else
        {
            throw new FormatException();
        }
    }

    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        if (destination.Length < 5)
        {
            charsWritten = 0;
            return false;
        }

        destination[0] = (char)(Correct + '0');
        destination[1] = ':';
        destination[2] = (char)(WrongPosition + '0');
        destination[3] = ':';
        destination[4] = (char)(ColorOrShape + '0');
        charsWritten = 5;
        return true;
    }
}
