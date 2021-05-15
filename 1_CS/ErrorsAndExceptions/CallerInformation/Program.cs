using System;
using System.Runtime.CompilerServices;

class Program
{
    static void Main()
    {
        Program p = new();
        p.Log();
        p.SomeProperty = 33;
        Action a1 = () => p.Log();
        a1();
    }

    private int _someProperty;
    public int SomeProperty
    {
        get => _someProperty;
        set
        {
            Log();
            _someProperty = value;
        }
    }

    public void Log([CallerLineNumber] int line = -1,
        [CallerFilePath] string? path = default,
        [CallerMemberName] string? name = default)
    {
        Console.WriteLine($"Line {line}");
        Console.WriteLine(path);
        Console.WriteLine(name);
        Console.WriteLine();
    }
}
