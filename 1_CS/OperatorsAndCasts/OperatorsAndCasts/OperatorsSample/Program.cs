using System;

PrefixAndPostfix();
ConditionalOperator();

try
{
    OverflowExceptionSample();
}
catch (OverflowException ex)
{
    Console.WriteLine(ex.Message);
}

IsOperator();
IsOperatorWithConstPattern();
SizeofSample();

void SizeofSample()
{
    Console.WriteLine(nameof(SizeofSample));
    Console.WriteLine(sizeof(int));

    unsafe
    {
        Console.WriteLine(sizeof(Point));
    }
    Console.WriteLine();
}

void AsOperatorSample()
{
    Console.WriteLine(nameof(AsOperatorSample));
    object o1 = "Some String";
    object o2 = 5;
    string? s1 = o1 as string; // s1 = "Some String"
    string? s2 = o2 as string; // s2 = null
    Console.WriteLine($"o1 as string assigns a string to s1: {s1}");
    Console.WriteLine($"o2 as string assigns null to s2 because o2 is not a string: {s2}");
    Console.WriteLine();
}


void IsOperatorWithConstPattern()
{
    Console.WriteLine(nameof(IsOperatorWithConstPattern));
    int i = 42;
    if (i is 42)
    {
        Console.WriteLine("i has the value 42");
    }

    object? o = null;
    if (o is null)
    {
        Console.WriteLine("o is null");
    }
    Console.WriteLine();

}

void IsOperator()
{
    Console.WriteLine(nameof(IsOperator));
    int i = 10;
#pragma warning disable CS0183 // 'is' expression's given expression is always of the provided type
    if (i is object) // always an object
#pragma warning restore CS0183 // 'is' expression's given expression is always of the provided type
    {
        Console.WriteLine("i is an object");
    }
    Console.WriteLine();
}

void OverflowExceptionSample()
{
    Console.WriteLine(nameof(OverflowException));
    byte b = byte.MaxValue;
    checked
    {
        b++;
    }
    Console.WriteLine(b);
    Console.WriteLine();
}

void ConditionalOperator()
{
    Console.WriteLine(nameof(ConditionalOperator));
    int x = 1;
    string s = x + " ";
    s += (x == 1 ? "man" : "men");
    Console.WriteLine(s);
    Console.WriteLine();
}

void PrefixAndPostfix()
{
    Console.WriteLine(nameof(PrefixAndPostfix));
    int x = 5;
    if (++x == 6) // true – x is incremented to 6 before the evaluation
    {
        Console.WriteLine("This will execute");
    }
    if (x++ == 6) // true – x is incremented to 7 after the evaluation
    {
        Console.WriteLine($"The value of x is: {x}"); // x has the value 7
    }
    Console.WriteLine();
}
