using static TrafficLight;

TypeAndDeclarationPattern();
string s = VariablesWithWhen("hello world", "hello");
Console.WriteLine(s);
TypePatternSample();
PatternWithLogicalOperator();
await PositionalPatternSampleAsync();
PropertyPatternSample();
ListPatternSample();

void TypeAndDeclarationPattern()
{
    object o = new Book("Professional C#", "Wrox Press");
    if (o is Book)  // type pattern
    {
        Console.WriteLine("o is a book");
    }
    
    if (o is Book b) // type with declaration pattern
    {
        Console.WriteLine($"o is a book with title {b.Title}");
    }
}

string VariablesWithWhen(string text, string start)
{
    return text switch
    {
        string s when s.StartsWith(start) => "firstmatch",
        string s when s.EndsWith(start) => "secondmatch",
        _ => "discardmatch"
    };
}

void TypePatternSample()
{
    static string TypePattern(object? o) =>
        o switch
        {
            null => "just null",  // const pattern
            Book b => $"a book with this title: {b.Title}", // type with declaration pattern
            42 => "a constant with value 42", // const pattern
            string[] { Length: > 3 } => "a string array with more than 3 elements", // type, property, and relational pattern
            string[] => "any other string array", // type pattern
            _ => "anything else" // discard pattern
        };

    Console.WriteLine($"{nameof(TypePatternSample)}");
    Console.WriteLine(TypePattern(null));
    Console.WriteLine(TypePattern(new Book("Professional C#", "Wiley")));
    Console.WriteLine(TypePattern(42));
    Console.WriteLine(TypePattern(new [] {"one", "two", "three", "four"}));
    Console.WriteLine(TypePattern(new[] {"one", "two"}));
    Console.WriteLine(TypePattern("more"));
    Console.WriteLine();
}

void PatternWithLogicalOperator()
{
    string LogicalOperators(Person p) =>
        p switch
        {
            { PhoneNumber: not null } and { PhoneNumber: not ""} => $"{p.FirstName} {p.LastName}, ({p.PhoneNumber})", // property, logical pattern
            _ => $"{p.FirstName} {p.LastName}"
        };

    Console.WriteLine(nameof(PatternWithLogicalOperator));
    Person p1 = new("Tom", "Turbo");
    Person p2 = new("Bruce", "Wayne", "+108154711");
    Console.WriteLine(LogicalOperators(p1));
    Console.WriteLine(LogicalOperators(p2));
    Console.WriteLine();
}

async Task PositionalPatternSampleAsync()
{
    static (TrafficLight Current, TrafficLight Previous) NextLight(TrafficLight current, TrafficLight previous) =>
        (current, previous) switch
        {
            (Red, _) => (Amber, current),  // positional pattern, can be used with tuples and deconstruct as well
            (Amber, Red) => (Green, current),
            (Green, _) => (Amber, current),
            (Amber, Green) => (Red, current),
            _ => (Amber, current)
        };

    Console.WriteLine(nameof(PositionalPatternSampleAsync));
    var currentLight = Red;
    var previousLight = Red;
    for (int i = 0; i < 10; i++)
    {
        (currentLight, previousLight) = NextLight(currentLight, previousLight);
        Console.WriteLine($"current light: {currentLight}");
        await Task.Delay(1000);
    }
    Console.WriteLine();
}

void PropertyPatternSample()
{
    string Check(Person person) =>
        person switch
        {
            { FirstName: "Clark" } => $"{person} is a Clark", // property pattern
            { Address: { City: "Smallville" } } => $"{person} is from Smallville",
            { Address.City: "Gotham City" } => $"{person} is from Gotham City",  // extended property pattern (C# 10)
            _ => $"{person} is not listed"
        };

    foreach (var p in GetPeople())
    {
        Console.WriteLine(Check(p));
    }
}

void ListPatternSample()
{
    string ListPattern(int[] list)
    {
        return list switch
        {
            [1, 2, 3] => "1, 2, and 3 in the list",  // list pattern (C# 11)
            [1, 2, .. var x, 5] => $"slice pattern {x}", // slice pattern
            { Length: > 3 } => "array with more than three elements", // property and relational pattern
            _ => "not listed"
        };
    }

    int[] one = { 1, 2, 3 };
    int[] two = { 1, 2, 4, 5, 6, 9, 11, 5 };
    Console.WriteLine(ListPattern(one));
    Console.WriteLine(ListPattern(two));
}


IEnumerable<Person> GetPeople() =>
    new[] 
    {
        new Person("Clark", "Kent", Address: new Address("Smallville", "USA")),
        new Person("Lois", "Lane", Address: new Address("Smallville", "USA")),
        new Person("Bruce", "Wayne", Address: new Address("Gotham City", "USA")),
        new Person("Alfred", "Pennyworth", Address: new Address("Gotham City", "USA")),
        new Person("Dick", "Grayson", Address: new Address("Gotham City", "USA")),
        new Person("Barry", "Allen", Address: new Address("Central City", "USA")),
    };

public enum TrafficLight
{
    Red,
    Amber,
    Green
}

public record Book(string Title, string Publisher);

public record Address(string City, string Country);

public record Person(string FirstName, string LastName, string? PhoneNumber = null, Address? Address = null)
{
    public override string ToString() => $"{FirstName} {LastName}";
}
