using DataLib;
using System;
using System.Linq;

namespace EnumerableSample
{
    public class FilterSamples
    {
        public static void SimpleFilter()
        {
            Console.WriteLine("filter with a LINQ query based on number wins and the country either Brazil or Austria");

            var racers = from r in Formula1.GetChampions()
                         where r.Wins > 15 && (r.Country == "Brazil" || r.Country == "Austria")
                         select r;

            foreach (var r in racers)
            {
                Console.WriteLine($"{r:A}");
            }
        }

        public static void FilterWithIndex()
        {
            Console.WriteLine("filter using an index - returning every second champion where the lastname stars with A");

            var racers = Formula1.GetChampions()
                .Where((r, index) => r.LastName.StartsWith("A") && index % 2 != 0);
            foreach (var r in racers)
            {
                Console.WriteLine($"{r:A}");
            }
        }

        public static void FilterWithMethods()
        {
            Console.WriteLine("filter with the Where method - champions from Brazil and Austria");

            var racers = Formula1.GetChampions()
                .Where(r => r.Wins > 15 && (r.Country == "Brazil" || r.Country == "Austria"));

            foreach (var r in racers)
            {
                Console.WriteLine($"{r:A}");
            }
        }

        public static void TypeFilter()
        {
            Console.WriteLine("query with a type filter - OfType - only getting strings");

            object[] data = { "one", 2, 3, "four", "five", 6 };
            var query = data.OfType<string>();
            foreach (var s in query)
            {
                Console.WriteLine(s);
            }
        }
    }
}
