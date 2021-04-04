using System;
using System.Collections.Generic;

namespace DataLib
{
    public record Racer(string FirstName, string LastName, string Country, int Starts, int Wins, IEnumerable<int> Years, IEnumerable<string> Cars) : IComparable<Racer>, IFormattable
    {
        public Racer(string FirstName, string LastName, string Country, int Starts, int Wins)
            : this(FirstName, LastName, Country, Starts, Wins, new int[] { }, new string[] { })
        {  }

        public override string ToString() => $"{FirstName} {LastName}";

        public int CompareTo(Racer? other) => LastName.CompareTo(other?.LastName);

        public string ToString(string format) => ToString(format, null);

        public string ToString(string? format, IFormatProvider? formatProvider) =>
            format switch
            {
                null => ToString(),
                "N" => ToString(),
                "F" => FirstName,
                "L" => LastName,
                "C" => Country,
                "S" => Starts.ToString(),
                "W" => Wins.ToString(),
                "A" => $"{FirstName} {LastName}, country: {Country}, starts: {Starts}, wins: {Wins}",
                _ => throw new FormatException($"Format {format} not supported")
            };        
    }
}