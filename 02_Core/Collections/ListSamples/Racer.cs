using System;

namespace ListSamples
{
    public class Racer : IComparable<Racer>, IFormattable
    {
        public Racer(int id, string firstName, string lastName, string country, int wins)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Country = country;
            Wins = wins;
        }

        public Racer(int id, string firstName, string lastName, string country)
          : this(id, firstName, lastName, country, wins: 0)
        { }

        public int Id { get; }
        public string? FirstName { get; }
        public string? LastName { get; }
        public string? Country { get; }
        public int Wins { get; set; }

        public override string ToString() => $"{FirstName} {LastName}";

        public string? ToString(string? format, IFormatProvider? formatProvider)
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
}