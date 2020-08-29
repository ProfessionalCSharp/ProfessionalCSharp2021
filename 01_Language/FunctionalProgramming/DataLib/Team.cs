using System.Collections.Generic;

namespace DataLib
{
    public record Team(string Name, IEnumerable<int> Years)
    {
        public Team(string name, params int[] years)
            : this(name, (IEnumerable<int>)years ?? new List<int>())
        {
        }
    }
}