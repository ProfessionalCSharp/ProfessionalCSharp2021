using System;

namespace StringFormats
{
    public record Person(string FirstName, string LastName) : IFormattable
    {
        public override string ToString() => FirstName + " " + LastName;

        public virtual string ToString(string format) => ToString(format, null);

        public string ToString(string? format, IFormatProvider? formatProvider) =>
            format switch
            {
                null => ToString(),
                "A" => ToString(),
                "F" => FirstName,
                "L" => LastName,
                _ => throw new FormatException($"invalid format string {format}")
            };
    }
}