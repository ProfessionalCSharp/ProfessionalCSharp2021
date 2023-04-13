using System.Diagnostics.CodeAnalysis;

namespace Codebreaker.Models;

public partial record ColorField : IParsable<ColorField>
{
    public static ColorField Parse(string s, IFormatProvider? provider = default)
    {
        if (TryParse(s, provider, out ColorField? color))
        {
            return color;
        }
        else
        {
            throw new ArgumentException($"Cannot parse value {s}", nameof(s));
        }
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out ColorField result)
    {
        if (s is null)
        {
            result = null;
            return false;
        }
        result = new ColorField(s);
        return true;
    }
}
