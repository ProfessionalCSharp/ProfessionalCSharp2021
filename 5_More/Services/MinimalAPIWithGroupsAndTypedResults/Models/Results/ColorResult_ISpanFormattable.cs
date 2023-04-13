namespace Codebreaker.Models;

public readonly partial record struct ColorResult : ISpanFormattable
{
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
