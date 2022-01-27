namespace EnumerableSample;

internal class LinqSamples
{
    public static void GenerateRange()
    {
        Console.WriteLine("Use the Range method to generate a list of values");

        var values = Enumerable.Range(1, 20);
        foreach (var item in values)
        {
            Console.Write($"{item} ", item);
        }
        Console.WriteLine();
    }

    public static void Prepend()
    {
        Console.WriteLine("Adding a racer before the result");
        var racersAndFirst = Formula1.GetChampions().Where(r => r.Country == "Austria").Prepend(new Racer("first", "First", "first", 0, 0));
        foreach (var r in racersAndFirst)
        {
            Console.WriteLine(r);
        }
    }

    public static void Except()
    {
        Console.WriteLine("Show the list of Formula 1 drives that have been in the top 3 but never a world champion");

        var racers = Formula1.GetChampionships().SelectMany(cs => new List<RacerInfo>()
               {
                 new RacerInfo(cs.Year, 1, cs.First.FirstName(), cs.First.LastName()),
                 new RacerInfo(cs.Year, 2, cs.Second.FirstName(), cs.Second.LastName()),
                 new RacerInfo(cs.Year, 3, cs.Third.FirstName(), cs.Third.LastName())
               });

        var nonChampions = racers.Select(r =>
          new
          {
              r.FirstName,
              r.LastName
          }).Except(Formula1.GetChampions().Select(r =>
            new
            {
                r.FirstName,
                r.LastName
            }));

        foreach (var r in nonChampions)
        {
            Console.WriteLine($"{r.FirstName} {r.LastName}");
        }
    }

    public static void ConvertWithCast()
    {
        Console.WriteLine("Convert elements of a non-generic collections using Cast<T>");

        if (Formula1.GetChampions() is System.Collections.ICollection coll)
        {
            var list = new System.Collections.ArrayList(coll);

            var query = from r in list.Cast<Racer>()
                        where r.Country == "USA"
                        orderby r.Wins descending
                        select r;
            foreach (var racer in query)
            {
                Console.WriteLine($"{racer:A}");
            }
        }
    }

    public static void Zip()
    {
        Console.WriteLine("Use Zip to combine collections - names of Italian champions and a second list with number of starts from Italian champions");

        var racerNames = from r in Formula1.GetChampions()
                         where r.Country == "Italy"
                         orderby r.Wins descending
                         select new
                         {
                             Name = r.FirstName + " " + r.LastName
                         };

        var racerNamesAndStarts = from r in Formula1.GetChampions()
                                  where r.Country == "Italy"
                                  orderby r.Wins descending
                                  select new
                                  {
                                      r.LastName,
                                      r.Starts
                                  };

        var racers = racerNames.Zip(racerNamesAndStarts, (first, second) => first.Name + ", starts: " + second.Starts);
        foreach (var r in racers)
        {
            Console.WriteLine(r);
        }
    }

    public static void ToLookup()
    {
        Console.WriteLine("ToLookup: create a Lookup for racers by car");

        var racers = (from r in Formula1.GetChampions()
                      from c in r.Cars!
                      select new
                      {
                          Car = c,
                          Racer = r
                      }).ToLookup(cr => cr.Car, cr => cr.Racer);

        foreach (var williamsRacer in racers["Williams"])
        {
            Console.WriteLine(williamsRacer);
        }
    }

    public static void AggregateSum()
    {
        Console.WriteLine("Use Sum to aggregate all from champions by country");

        var countries = (from c in
                             from r in Formula1.GetChampions()
                             group r by r.Country into c
                             select new
                             {
                                 Country = c.Key,
                                 Wins = (from r1 in c
                                         select r1.Wins).Sum()
                             }
                         orderby c.Wins descending, c.Country
                         select c).Take(5);

        foreach (var country in countries)
        {
            Console.WriteLine($"{country.Country} {country.Wins}");
        }
    }

    public static void AggregateCount()
    {
        Console.WriteLine("Use Count to sum the number of championships from a champion");

        var query = from r in Formula1.GetChampions()
                    let numberYears = r.Years?.Count()
                    where numberYears >= 3
                    orderby numberYears descending, r.LastName
                    select new
                    {
                        Name = $"{r.FirstName} {r.LastName}",
                        TimesChampion = numberYears
                    };

        foreach (var r in query)
        {
            Console.WriteLine($"{r.Name} {r.TimesChampion}");
        }
    }

    public static void Partitioning()
    {
        Console.WriteLine("split returned data from a query with Skip and Take methods");

        int pageSize = 5;

        int numberPages = (int)Math.Ceiling(Formula1.GetChampions().Count() /
              (double)pageSize);

        for (int page = 0; page < numberPages; page++)
        {
            Console.WriteLine($"Page {page}");

            var racers =
               (from r in Formula1.GetChampions()
                orderby r.LastName, r.FirstName
                select r.FirstName + " " + r.LastName)
                .Skip(page * pageSize).Take(pageSize);

            foreach (var name in racers)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();
        }
    }

    public static void Range()
    {
        Console.WriteLine("return the last three");

        var racers =
           (from r in Formula1.GetChampions()
            orderby r.LastName, r.FirstName
            select r.FirstName + " " + r.LastName)
            .Take(^3..);

        foreach (var name in racers)
        {
            Console.WriteLine(name);
        }
        Console.WriteLine();
    }

    public static void Chunks()
    {
        Console.WriteLine("Create 5 element chunks");

        var racersArray = Formula1.GetChampions()
            .OrderBy(r => r.LastName)
            .ThenBy(r => r.FirstName)
            .Select(r => $"{r.FirstName} {r.LastName}")
            .Chunk(5);

        foreach (var chunk in racersArray)
        {
            Console.WriteLine("chunk...");
            foreach (var racer in chunk)
            {
                Console.WriteLine(racer);
            }
            Console.WriteLine();
        }
    }

    public static void SetOperations()
    {
        IEnumerable<Racer> racersByCar(string car) =>
            from r in Formula1.GetChampions()
            from c in r.Cars!
            where c == car
            orderby r.LastName
            select r;

        Console.WriteLine("Use intersect on two queries with a local function to return world champions both with Ferrari and McLaren");

        foreach (var racer in racersByCar("Ferrari").Intersect(racersByCar("McLaren")))
        {
            Console.WriteLine(racer);
        }
    }

    public static void ToList()
    {
        Console.WriteLine("early evaluation to return a List<T>");

        List<Racer> racers = (from r in Formula1.GetChampions()
                              where r.Starts > 200
                              orderby r.Starts descending
                              select r).ToList();

        foreach (var racer in racers)
        {
            Console.WriteLine($"{racer} {racer:S}");
        }
    }
}
