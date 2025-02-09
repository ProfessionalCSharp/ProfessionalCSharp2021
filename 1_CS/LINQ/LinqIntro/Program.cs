﻿global using DataLib;

LINQQuery();
ExtensionMethods();
DeferredQuery();

void DeferredQuery()
{
    List<string> names = ["Nino", "Alberto", "Juan", "Mike", "Phil"];

    var namesWithJ = from n in names
                     where n.StartsWith('J')
                     orderby n
                     select n;

    Console.WriteLine("First iteration");
    foreach (string name in namesWithJ)
    {
        Console.WriteLine(name);
    }
    Console.WriteLine();

    names.Add("John");
    names.Add("Jim");
    names.Add("Jack");
    names.Add("Denny");

    Console.WriteLine("Second iteration");
    foreach (string name in namesWithJ)
    {
        Console.WriteLine(name);
    }
    Console.WriteLine();
}

void ExtensionMethods()
{
    List<Racer> champions = [.. Formula1.GetChampions()];
    var brazilChampions =
        champions.Where(r => r.Country == "Brazil")
            .OrderByDescending(r => r.Wins)
            .Select(r => r);

    foreach (Racer r in brazilChampions)
    {
        Console.WriteLine($"{r:A}");
    }
    Console.WriteLine();
}

void LINQQuery()
{
    var query = from r in Formula1.GetChampions()
                where r.Country == "Brazil"
                orderby r.Wins descending
                select r;

    foreach (var r in query)
    {
        Console.WriteLine($"{r:A}");
    }
    Console.WriteLine();
}
