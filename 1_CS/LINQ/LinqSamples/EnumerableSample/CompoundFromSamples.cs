using DataLib;
using System;
using System.Linq;

namespace EnumerableSample
{
    class CompoundFromSamples
    {
        public static void CompoundFrom()
        {
            Console.WriteLine("compound from with racers and cars - retrieving Formula champions with a Ferrari");
            var ferrariDrivers = from r in Formula1.GetChampions()
                                 from c in r.Cars
                                 where c == "Ferrari"
                                 orderby r.LastName
                                 select $"{r.FirstName} {r.LastName}";

            foreach (var racer in ferrariDrivers)
            {
                Console.WriteLine(racer);
            }
        }

        public static void CompoundFromWithMethods()
        {
            Console.WriteLine("compound from with the SelectMany method - retrieving Formula champions with a Ferrari");

            var ferrariDrivers = Formula1.GetChampions()
                .SelectMany(r => r.Cars, (r1, cars) => new { Racer1 = r1, Cars1 = cars })
                .Where(item => item.Cars1.Contains("Ferrari"))
                .OrderBy(item => item.Racer1.LastName)
                .Select(item => $"{item.Racer1.FirstName} {item.Racer1.LastName}");

            foreach (var racer in ferrariDrivers)
            {
                Console.WriteLine(racer);
            }
        }
    }
}
