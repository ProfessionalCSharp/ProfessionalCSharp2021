﻿namespace Codebreaker.Models;

public record ColorField(string Color) : IParsable<ColorField>
{
    public override string ToString() => Color;

    public static ColorField Parse(string s, IFormatProvider? provider)
    {
        if (TryParse(s, null, out ColorField? color))
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
