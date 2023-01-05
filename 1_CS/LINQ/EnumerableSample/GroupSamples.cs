namespace EnumerableSample;

class GroupSamples
{
    public static void Grouping()
    {
        Console.WriteLine("LINQ query with group by into - retrieving countries of champions, and the number of champions from a country");
        var countries = from r in Formula1.GetChampions()
                        group r by r.Country into g
                        orderby g.Count() descending, g.Key
                        where g.Count() >= 2
                        select new
                        {
                            Country = g.Key,
                            Count = g.Count()
                        };

        foreach (var item in countries)
        {
            Console.WriteLine($"{item.Country,-10} {item.Count}");
        }
    }

    public static void GroupingWithMethods()
    {
        Console.WriteLine("LINQ query with the GroupBy method - retrieving countries of champions, and the number of champions from a country");

        var countries = Formula1.GetChampions()
          .GroupBy(r => r.Country)
          .OrderByDescending(g => g.Count())
          .ThenBy(g => g.Key)
          .Where(g => g.Count() >= 2)
          .Select(g => new
          {
              Country = g.Key,
              Count = g.Count()
          });

        foreach (var item in countries)
        {
            Console.WriteLine($"{item.Country,-10} {item.Count}");
        }
    }

    public static void GroupingWithVariables()
    {
        Console.WriteLine("using a variable with a LINQ query");

        var countries = from r in Formula1.GetChampions()
                        group r by r.Country into g
                        let count = g.Count()
                        orderby count descending, g.Key
                        where count >= 2
                        select new
                        {
                            Country = g.Key,
                            Count = count
                        };

        foreach (var item in countries)
        {
            Console.WriteLine($"{item.Country,-10} {item.Count}");
        }
    }

    public static void GroupingWithAnonymousTypes()
    {
        Console.WriteLine("returning an anonymous type from a query");

        var countries = Formula1.GetChampions()
          .GroupBy(r => r.Country)
          .Select(g => new { Group = g, Count = g.Count() })
          .OrderByDescending(g => g.Count)
          .ThenBy(g => g.Group.Key)
          .Where(g => g.Count >= 2)
          .Select(g => new
          {
              Country = g.Group.Key,
              g.Count
          });

        foreach (var item in countries)
        {
            Console.WriteLine($"{item.Country,-10} {item.Count}");
        }
    }

    public static void GroupingWithTuples()
    {
        Console.WriteLine("returning a tuple from a query");

        var countries = Formula1.GetChampions()
          .GroupBy(r => r.Country)
          .Select(g => new { Group = g, Count = g.Count() })
          .OrderByDescending(g => g.Count)
          .ThenBy(g => g.Group.Key)
          .Where(g => g.Count >= 2)
          .Select(g =>
          (
              Country: g.Group.Key,
              g.Count
          ));

        foreach (var item in countries)
        {
            Console.WriteLine($"{item.Country,-10} {item.Count}");
        }
    }

    public static void GroupingAndNestedObjects()
    {
        Console.WriteLine("returning nested objects with a LINQ query");

        var countries = from r in Formula1.GetChampions()
                        group r by r.Country into g
                        let count = g.Count()
                        orderby count descending, g.Key
                        where count >= 2
                        select new
                        {
                            Country = g.Key,
                            Count = count,
                            Racers = from r1 in g
                                     orderby r1.LastName
                                     select r1.FirstName + " " + r1.LastName
                        };

        foreach (var item in countries)
        {
            Console.WriteLine($"{item.Country,-10} {item.Count}");
            foreach (var name in item.Racers)
            {
                Console.Write($"{name}; ");
            }
            Console.WriteLine();
        }
    }

    public static void GroupingAndNestedObjectsWithMethods()
    {
        Console.WriteLine("Using method syntax to return nested objects");

        var countries = Formula1.GetChampions()
            .GroupBy(r => r.Country)
            .Select(g => new
            {
                Group = g,
                g.Key,
                Count = g.Count()
            })
            .OrderByDescending(g => g.Count)
            .ThenBy(g => g.Key)
            .Where(g => g.Count >= 2)
            .Select(g => new
            {
                Country = g.Key,
                g.Count,
                Racers = g.Group.OrderBy(r => r.LastName).Select(r => r.FirstName + " " + r.LastName)
            });

        foreach (var item in countries)
        {
            Console.WriteLine($"{item.Country,-10} {item.Count}");
            foreach (var name in item.Racers)
            {
                Console.Write($"{name}; ");
            }
            Console.WriteLine();
        }
    }
}
