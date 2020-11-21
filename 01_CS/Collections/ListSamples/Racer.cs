using System;

public record Racer(int id, string FirstName, string LastName, string Country, int Wins) : IComparable<Racer>, IFormattable
{
    public Racer(int id, string firstName, string lastName, string country)
      : this(id, firstName, lastName, country, Wins: 0)
    { }

    public override string ToString() => $"{FirstName} {LastName}";

    public string ToString(string? format, IFormatProvider? formatProvider)
        => format?.ToUpper() switch
        {
            null => ToString(),
            "N" => ToString(),
            "F" => FirstName,
            "L" => LastName,
            "W" => $"{ToString()}, Wins: {Wins}",
            "C" => Country,
            "A" => $"{ToString()}, Country: {Country}, Wins: {Wins}",
            _ => throw new FormatException(string.Format(formatProvider,
                      "Format {0} is not supported", format))
        };

    public string? ToString(string format) => ToString(format, null);

    public int CompareTo(Racer? other)
    {
        int compare = LastName?.CompareTo(other?.LastName) ?? -1;
        if (compare == 0)
        {
            return FirstName?.CompareTo(other?.FirstName) ?? -1;
        }
        return compare;
    }
}
