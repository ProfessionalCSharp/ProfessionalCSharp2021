using System;

IfStatement();
Book b = new("Professional C#");
PatternMatching(b);


void PatternMatching(object o)
{
    if (o is null) throw new ArgumentNullException(nameof(o));
    else if (o is Book b)
    {
        Console.WriteLine($"received a book: {b.Title}");
    }
}

void IfStatement()
{

    Console.WriteLine("Type in a string");
    string? input = Console.ReadLine();

    if (input == string.Empty)
    {
        Console.WriteLine("You typed in an empty string.");
    }
    else if (input?.Length < 5)
    {
        Console.WriteLine("The string had less than 5 characters.");
    }
    else
    {
        Console.WriteLine("Read any other string");
    }
    Console.WriteLine("The string was " + input);
}

class Book
{
    public Book(string title) => Title = title;
    public string Title { get; }
}
