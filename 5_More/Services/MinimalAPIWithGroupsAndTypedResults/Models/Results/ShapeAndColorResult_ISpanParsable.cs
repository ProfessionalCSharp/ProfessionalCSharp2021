using System.Diagnostics.CodeAnalysis;

namespace Codebreaker.Models;

public readonly partial record struct ShapeAndColorResult : ISpanParsable<ShapeAndColorResult>
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
}
