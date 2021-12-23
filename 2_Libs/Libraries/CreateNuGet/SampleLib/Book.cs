using System.Runtime.CompilerServices;

namespace SampleLib;

public class Book
{
    public string? Title { get; set; }
    public string? Publisher { get; set; }

#if NET5_0_OR_GREATER
    [ModuleInitializer]
    internal static void Initializer()
    {
        Console.WriteLine("Module Initializer with Book");
    }
#endif
}
