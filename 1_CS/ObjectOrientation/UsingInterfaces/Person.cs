using System;

public record Person(string FirstName, string LastName) : IComparable<Person>
{
    public int CompareTo(Person? other)
    {
        int compare = LastName.CompareTo(other?.LastName);
        if (compare is 0)
        {
            return FirstName.CompareTo(other?.FirstName);
        }
        return compare;
    }
}
