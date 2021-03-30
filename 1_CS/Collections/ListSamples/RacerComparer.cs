using System;
using System.Collections.Generic;

public enum CompareType
{
    FirstName,
    LastName,
    Country,
    Wins
}

public class RacerComparer : IComparer<Racer>
{
    private CompareType _compareType;
    public RacerComparer(CompareType compareType) =>
      _compareType = compareType;

    public int Compare(Racer? x, Racer? y)
    {
        if (x is null && y is null) return 0;
        if (x is null) return -1;
        if (y is null) return 1;

        int CompareCountry(Racer x, Racer y)
        {
            int result = string.Compare(x.Country, y.Country);
            if (result == 0)
            {
                result = string.Compare(x.LastName, y.LastName);
            }
            return result;
        }

        return _compareType switch
            {
                CompareType.FirstName => string.Compare(x.FirstName, y.FirstName),
                CompareType.LastName => string.Compare(x.LastName, y.LastName),
                CompareType.Country => CompareCountry(x, y),
                CompareType.Wins => x.Wins.CompareTo(y.Wins),
                _ => throw new ArgumentException("Invalid Compare Type")
            };
    }
}