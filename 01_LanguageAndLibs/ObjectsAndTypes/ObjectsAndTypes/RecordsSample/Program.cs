using System;

namespace RecordsSample
{
    // positional record
    public record Book1(string Title, string Publisher) { }

    // implements Equals, ==, != operators, GetHashCode, properties with get/init, Deconstruct

    // nominal records - would be init-only properties, but this seems to have changed
    //public record Book2 { string Title; string Publisher; }
    public record Book2
    {
        public string Title { get; init; } = string.Empty;
        public string Publisher { get; init; } = string.Empty;
    }

    public record Book3(string Title, string Publisher) : Book1(Title, Publisher)
    {
        public Type GetContract() => EqualityContract;
    }

    class Program
    {
        static void Main()
        {
            var b1 = new Book1("Professional C# 7", "Wrox Press");
            (string title, string publisher) = b1;
            
            var b1b = b1 with { Title = "Professional C# 9" };
            Console.WriteLine(b1b);

            var b3 = new Book1("Professional C# 7", "Wrox Press");
            if (b1 == b3)
                Console.WriteLine("the books are the same");            
            
            var b2 = new Book2 { Title = "one", Publisher = "two" };
            
            var b2b = b2 with { Title = "two" };
            var b2c = b2b with { Title = "one" };
            if (b2 == b2c) Console.WriteLine("the same");

            var b4 = new Book3("one", "two");
            Console.WriteLine(b4.GetContract().Name);
            
        }
    }
}
