using System.Diagnostics.CodeAnalysis;

namespace Codebreaker.Models;

public enum ResultInformation
{
    Incorrect,
    CorrectPositionAndColor,
    CorrectColor
}

public readonly record struct SimpleColorResult(ResultInformation[] Results) : ISpanParsable<SimpleColorResult>
{
    public static SimpleColorResult Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public static SimpleColorResult Parse(string s, IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out SimpleColorResult result)
    {
        result = s switch
        {
            
            _ => default
        };

        return s != default;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out SimpleColorResult result) => 
        TryParse(s.AsSpan(), provider, out result);
}
