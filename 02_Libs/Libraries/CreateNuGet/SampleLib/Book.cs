using System;
using System.Runtime.CompilerServices;

namespace SampleLib
{
    public class Book
    {
        public string? Title { get; set; }
        public string? Publisher { get; set; }

#if NET50
        [ModuleInitializer]
        internal static void Initializer()
        {
            Console.WriteLine("Module Initializer with Book");
        }
#endif
    }
}
