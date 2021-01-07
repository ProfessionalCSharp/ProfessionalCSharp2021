using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace ResourcesDemo
{
    class Program
    {
        static void Main()
        {
            ResourceManager resources = new("ResourcesDemo.Resources.Messages", typeof(Program).GetTypeInfo().Assembly);
            string? goodMorning = resources.GetString("GoodMorning", new CultureInfo("de-AT"));
            Console.WriteLine(goodMorning);

            ResourceManager programResources = new(typeof(Program));
            Console.WriteLine(programResources.GetString("Resource1"));
        }
    }
}
