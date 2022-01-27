UsingStringBuilder();
InterpolatedStrings();
UsingFormattableString();
UseStringFormat();
UseVerbatimStrings();
RangesWithStrings();

void UsingStringBuilder()
{
    StringBuilder sb = new("the quick");
    sb.Append(' ');
    sb.Append("brown fox jumped over ");
    sb.Append("the lazy dogs 1234567890 times");
    string s = sb.ToString();
    Console.WriteLine(s);
    Console.WriteLine();
}

void InterpolatedStrings()
{
    string s1 = "World";
    string s2 = $"Hello, {s1}!";

    Console.WriteLine(s2);

    int x = 3, y = 4;
    string s3 = $"The result of {x} and {y} is {x + y}";
    Console.WriteLine(s3);
    Console.WriteLine();
}

void UsingFormattableString()
{
    int x = 3, y = 4;
    FormattableString s = $"The result of {x} + {y} is {x + y}";
    Console.WriteLine($"format: {s.Format}");
    for (int i = 0; i < s.ArgumentCount; i++)
    {
        Console.WriteLine($"argument: {i}:{s.GetArgument(i)}");
    }
    Console.WriteLine();
}

void UseStringFormat()
{
    DateTime day = new(2025, 2, 14);
    Console.WriteLine($"{day:D}");
    Console.WriteLine($"{day:d}");

    int i = 2477;
    Console.WriteLine($"{i:n} {i:e} {i:x} {i:c}");

    double d = 3.1415;
    Console.WriteLine($"{d:###.###}");
    Console.WriteLine($"{d:000.000}");
    Console.WriteLine();
}

void UseVerbatimStrings()
{
    string s = @"a tab: \t, a carriage return: \r, a newline: \n";
    Console.WriteLine(s);
    Console.WriteLine();
}

void RangesWithStrings()
{
    string s = "The quick brown fox jumped over the lazy dogs down " +
        "1234567890 times";
        string the = s[..3];
    string quick = s[4..9];
    string times = s[^5..^0];
    Console.WriteLine(the);
    Console.WriteLine(quick);
    Console.WriteLine(times);
    Console.WriteLine();
}