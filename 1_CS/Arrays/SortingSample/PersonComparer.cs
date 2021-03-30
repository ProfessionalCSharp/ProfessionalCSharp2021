using System;
using System.Collections.Generic;

namespace Wrox.ProCSharp.Arrays
{
    public enum PersonCompareType
    {
        FirstName,
        LastName
    }

    public class PersonComparer : IComparer<Person>
    {
        private PersonCompareType _compareType;

        public PersonComparer(PersonCompareType compareType) => 
            _compareType = compareType;

        #region IComparer<Person> Members
        public int Compare(Person? x, Person? y)
        {
            if (x is null && y is null) return 0;
            if (x is null) return 1;
            if (y is null) return -1;

            return _compareType switch
            {
                PersonCompareType.FirstName => x.FirstName.CompareTo(y.FirstName),
                PersonCompareType.LastName => x.LastName.CompareTo(y.LastName),
                _ => throw new ArgumentException("unexpected compare type")
            };
        }

        #endregion
    }
}
