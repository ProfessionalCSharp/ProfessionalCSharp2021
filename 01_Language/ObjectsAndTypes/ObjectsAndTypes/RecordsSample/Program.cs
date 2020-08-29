using System;

namespace RecordsSample
{
    // positional record
    public record Book1(string Title, string Publisher) { }

    // implements Equals, ==, != operators, GetHashCode

    // nominal records
    public record Book2
    {
        public string Title { get; init; }
        public string Publisher { get; init; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var b1 = new Book1("one", "two");
            
            
            var b2 = new Book2 { Title = "one", Publisher = "two" };
        }
    }
}
