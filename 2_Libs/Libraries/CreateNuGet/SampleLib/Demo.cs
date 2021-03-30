using System;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace SampleLib
{
    public class Demo
    {
#if NETSTANDARD20
        private static string s_info = ".NET Standard 2.0";
#elif NET50
        private static string s_info = ".NET 5.0";
#else
        private static string s_info = "Unknown";
#endif

        public static string Show() => s_info;

        public static string GetJson(Book book) =>
            JsonSerializer.Serialize(book);

#if NET50
        [ModuleInitializer]
        internal static void Initializer()
        {
            Console.WriteLine("Module Initializer");
        }
#endif
    }
}
