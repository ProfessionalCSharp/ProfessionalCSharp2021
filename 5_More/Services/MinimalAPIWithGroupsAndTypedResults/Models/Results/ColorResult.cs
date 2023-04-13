using System.Diagnostics.CodeAnalysis;

namespace Codebreaker.Models;

public readonly record struct ColorResult(byte Correct, byte WrongPosition) : ISpanParsable<ColorResult>, ISpanFormattable
{
    public static ColorResult Parse(ReadOnlySpan<char> s, IFormatProvider? provider = default)
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

    public static ColorResult Parse(string s, IFormatProvider? provider = default) =>
        Parse(s.AsSpan(), provider);

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out ColorResult result)
    {
        result = s switch
        {
            { Length: > 3 or < 3 } => default,
            [ var correct, ':', var wrongpos ] => new ColorResult((byte)(correct - '0'), (byte)(wrongpos - '0')),          
            _ => default,
        };

        return result != default;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out ColorResult result) =>
        TryParse(s.AsSpan(), provider, out result);

    public string ToString(string? format, IFormatProvider? formatProvider = default)
    {
        var destination = new char[3].AsSpan();
        if (TryFormat(destination, out _, format.AsSpan(), formatProvider))
        {
            return destination.ToString();
        }
        else
        {
            throw new FormatException();
        }
    }

    // If just IFormattable would be used - without ISpanFormattable^, this is the implementation
    // public string ToString(string? format, IFormatProvider? formatProvider = default) => 
    //    $"{Correct}:{WrongPosition}";

    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default, IFormatProvider? provider = default)
    {
        if (destination.Length < 3)
        {
            charsWritten = 0;
            return false;
        }

        destination[0] = (char)(Correct + '0');
        destination[1] = ':';
        destination[2] = (char)(WrongPosition + '0');
        charsWritten = 3;
        return true;
    }
}
