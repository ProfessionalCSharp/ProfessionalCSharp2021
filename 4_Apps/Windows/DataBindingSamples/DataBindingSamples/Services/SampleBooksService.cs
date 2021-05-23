using DataBindingSamples.Models;
using System.Collections.Generic;

namespace DataBindingSamples.Services
{
    public class SampleBooksService
    {
        private readonly List<Book> _books = new()
        {
            new(1, "Professional C# and .NET - 2021 Edition", "Wrox Press", "Christian Nagel"),
            new(2, "Professional C# 7 and .NET Core 2", "Wrox Press", "Christian Nagel"),
            new(3, "Professional C# 6 and .NET Core 1.0", "Wrox Press", "Christian Nagel"),
            new(4, "Professional C# 5.0 and .NET 4.5.1", "Wrox Press", "Christian Nagel", "Jay Glynn", "Morgan Skinner"),
            new(5, "Enterprise Services with the .NET Framework", "AWL", "Christian Nagel")
        };
        public IEnumerable<Book> GetSampleBooks() => _books;
    }
}
