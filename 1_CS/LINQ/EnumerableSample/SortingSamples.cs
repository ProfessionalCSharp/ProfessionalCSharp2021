namespace EnumerableSample;

class SortingSamples
{
    // .NET 7 update
    public static void SortDefault()
    {
        Console.WriteLine("Sort all the racers by the default sort as implemented in the Racer type");
        Console.WriteLine();

        var racers = Formula1.GetChampions().Order().Take(10);

        foreach (var racer in racers)
        {
            Console.WriteLine($"{racer.LastName}, {racer.FirstName}");
        }
    }

    public static void SortMultiple()
    {
        Console.WriteLine("Show the first 10 champions ordered by country, lastname, firstname");
        Console.WriteLine();

        var racers = (from r in Formula1.GetChampions()
                      orderby r.Country, r.LastName, r.FirstName
                      select r).Take(10);

        foreach (var racer in racers)
        {
            Console.WriteLine($"{racer.Country}: {racer.LastName}, {racer.FirstName}");
        }
    }

    public static void SortMultipleWithMethods()
    {
        var racers = Formula1.GetChampions()
                        .OrderBy(r => r.Country)
                        .ThenBy(r => r.LastName)
                        .ThenBy(r => r.FirstName)
                        .Take(10);

        foreach (var racer in racers)
        {
            Console.WriteLine($"{racer.Country}: {racer.LastName}, {racer.FirstName}");
        }
    }

    public static void SortDescending()
    {
        Console.WriteLine("Show all champions from Brazil ordered by wins descending");
        Console.WriteLine();

        var racers = from r in Formula1.GetChampions()
                     where r.Country == "Brazil"
                     orderby r.Wins descending
                     select r;

        foreach (var r in racers)
        {
            Console.WriteLine($"{r:A}");
        }
    }

    public static void SortDescendingWithMethods()
    {
        Console.WriteLine("Show all champions from Brazil ordered by wins descending");
        Console.WriteLine();
        var racers = Formula1.GetChampions()
            .Where(r => r.Country == "Brazil")
            .OrderByDescending(r => r.Wins);

        foreach (var r in racers)
        {
            Console.WriteLine($"{r:A}");
        }
    }
}
