namespace Codebreaker.Models;

public readonly partial record struct ShapeAndColorResult : ISpanFormattable
{
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
