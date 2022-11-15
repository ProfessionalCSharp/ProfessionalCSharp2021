using System.Runtime.CompilerServices;
using System.Text.Json;

namespace SampleLib;

public class Demo
{
#if NETSTANDARD20
    private readonly static string s_info = ".NET Standard 2.0";
#elif NET6_0
    private readonly static string s_info = ".NET 6.0";
#elif NET7_0
    private readonly static string s_info = ".NET 7.0";
#else
    private readonly static string s_info = "Unknown";
#endif

    public static string Show() => s_info;

    public static string GetJson(Book book) =>
        JsonSerializer.Serialize(book);

#if NET5_0_OR_GREATER
    [ModuleInitializer]
    internal static void Initializer()
    {
        Console.WriteLine("Module Initializer");
    }
#endif
}
