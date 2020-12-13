using System;

Book2 book2 = new("Professional C#", "Wrox Press");
Console.WriteLine(book2);

Book1 book1a = new() { Title = "Professional C#", Publisher = "Wrox Press" };
Book1 book1b = new() { Title = "Professional C#", Publisher = "Wrox Press" };
if (!object.ReferenceEquals(book1a, book1b)) Console.WriteLine("Two different references for equal records");
if (book1a == book1b) Console.WriteLine("Both records have the same values");

var aNewBook = book1a with { Title = "Professional C# and .NET - 2024" };


//var b2 = new Book2("Professional C# 7", "Wrox Press");
//var b2b = b2 with { Publisher = "xx" };
//(string title, string publisher) = b2;

//var b1b = b2 with { Title = "Professional C# 9" };
//Console.WriteLine(b1b);

//var b3 = new Book2("Professional C# 7", "Wrox Press");
//if (b2 == b3)
//    Console.WriteLine("the books are the same");



//var b2b = b2 with { Title = "two" };
//var b2c = b2b with { Title = "one" };
//if (b2 == b2c) Console.WriteLine("the same");

//var b4 = new Book3("one", "two");
//Console.WriteLine(b4.GetContract().Name);


// nominal records - would be init-only properties, but this seems to have changed
//public record Book2 { string Title; string Publisher; }
public record Book1
{
    public string Title { get; init; } = string.Empty;
    public string Publisher { get; init; } = string.Empty;
}

// positional record
public record Book2(string Title, string Publisher);

// implements Equals, ==, != operators, GetHashCode, properties with get/init, Deconstruct

public record Book3(string Title, string Publisher) : Book2(Title, Publisher)
{
    public Type GetContract() => EqualityContract;
}
