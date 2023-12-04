List<Racer> racers =
[
    new Racer(26, "Jacques", "Villeneuve", "Canada", 11),
    new Racer(18, "Alan", "Jones", "Australia", 12),
    new Racer(11, "Jackie", "Stewart", "United Kingdom", 27),
    new Racer(15, "James", "Hunt", "United Kingdom", 10),
    new Racer(5, "Jack", "Brabham", "Australia", 14),
];

var lookupRacers = racers.ToLookup(r => r.Country);

foreach (Racer r in lookupRacers["Australia"])
{
    Console.WriteLine(r);
}
