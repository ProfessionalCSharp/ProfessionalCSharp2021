SwitchSample(2);
SwitchWithPatternMatching(null);

void SwitchWithPatternMatching(object? o)
{
    switch (o)
    {
        case null:
            Console.WriteLine("const pattern with null");
            break;
        case int i when i > 42:
            Console.WriteLine("type pattern with when clause and a relational pattern");
            break;
        case int:
            Console.WriteLine("type pattern with an int");
            break;
        case Book b:
            Console.WriteLine($"type pattern with a Book {b.Title}");
            break;
        default:
            break;
    }
}

void SwitchSample(int x)
{
    switch (x)
    {
        case 1:
            Console.WriteLine("integerA = 1");
            break;
        case 2:
            Console.WriteLine("integerA = 2");
            goto case 3;
        case 3:
            Console.WriteLine("integerA = 3");
            break;
        default:
            Console.WriteLine("integerA is not 1, 2, or 3");
            break;
    }
}

class Book
{
    public Book(string title) => Title = title;
    public string Title { get; }
}
