using System;
using System.Collections.Generic;

public interface IEnumerableEx<T> : IEnumerable<T>
{
    public IEnumerable<T> Where(Func<T, bool> pred)
    {
        foreach (T item in this)
        {
            if (pred(item))
            {
                yield return item;
            }
        }
    }
}

