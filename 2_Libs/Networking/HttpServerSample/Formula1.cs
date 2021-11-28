namespace HttpServerSample;

public class Formula1
{
    private List<Racer>? _racers;
    public IEnumerable<Racer> GetChampions() => _racers ??= InitializeRacers();

    private static List<Racer> InitializeRacers() => new()
    {
        new("Nino", "Farina", "Italy", 33, 5, new[] { 1950 }, new[] { "Alfa Romeo" }),
        new("Alberto", "Ascari", "Italy", 32, 13, new[] { 1952, 1953 }, new[] { "Ferrari" }),
        new("Juan Manuel", "Fangio", "Argentina", 51, 24, new int[] { 1951, 1954, 1955, 1956, 1957 }, new string[] { "Alfa Romeo", "Maserati", "Mercedes", "Ferrari" }),
        new("Mike", "Hawthorn", "UK", 45, 3, new int[] { 1958 }, new string[] { "Ferrari" }),
        new("Phil", "Hill", "USA", 48, 3, new int[] { 1961 }, new string[] { "Ferrari" }),
        new("John", "Surtees", "UK", 111, 6, new int[] { 1964 }, new string[] { "Ferrari" }),
        new("Jim", "Clark", "UK", 72, 25, new int[] { 1963, 1965 }, new string[] { "Lotus" }),
        new("Jack", "Brabham", "Australia", 125, 14, new int[] { 1959, 1960, 1966 }, new string[] { "Cooper", "Brabham" }),
        new("Denny", "Hulme", "New Zealand", 112, 8, new int[] { 1967 }, new string[] { "Brabham" }),
        new("Graham", "Hill", "UK", 176, 14, new int[] { 1962, 1968 }, new string[] { "BRM", "Lotus" }),
        new("Jochen", "Rindt", "Austria", 60, 6, new int[] { 1970 }, new string[] { "Lotus" }),
        new("Jackie", "Stewart", "UK", 99, 27, new int[] { 1969, 1971, 1973 }, new string[] { "Matra", "Tyrrell" }),
        new("Emerson", "Fittipaldi", "Brazil", 143, 14, new int[] { 1972, 1974 }, new string[] { "Lotus", "McLaren" }),
        new("James", "Hunt", "UK", 91, 10, new int[] { 1976 }, new string[] { "McLaren" }),
        new("Mario", "Andretti", "USA", 128, 12, new int[] { 1978 }, new string[] { "Lotus" }),
        new("Jody", "Scheckter", "South Africa", 112, 10, new int[] { 1979 }, new string[] { "Ferrari" }),
        new("Alan", "Jones", "Australia", 115, 12, new int[] { 1980 }, new string[] { "Williams" }),
        new("Keke", "Rosberg", "Finland", 114, 5, new int[] { 1982 }, new string[] { "Williams" }),
        new("Niki", "Lauda", "Austria", 173, 25, new int[] { 1975, 1977, 1984 }, new string[] { "Ferrari", "McLaren" }),
        new("Nelson", "Piquet", "Brazil", 204, 23, new int[] { 1981, 1983, 1987 }, new string[] { "Brabham", "Williams" }),
        new("Ayrton", "Senna", "Brazil", 161, 41, new int[] { 1988, 1990, 1991 }, new string[] { "McLaren" }),
        new("Nigel", "Mansell", "UK", 187, 31, new int[] { 1992 }, new string[] { "Williams" }),
        new("Alain", "Prost", "France", 197, 51, new int[] { 1985, 1986, 1989, 1993 }, new string[] { "McLaren", "Williams" }),
        new("Damon", "Hill", "UK", 114, 22, new int[] { 1996 }, new string[] { "Williams" }),
        new("Jacques", "Villeneuve", "Canada", 165, 11, new int[] { 1997 }, new string[] { "Williams" }),
        new("Mika", "Hakkinen", "Finland", 160, 20, new int[] { 1998, 1999 }, new string[] { "McLaren" }),
        new("Michael", "Schumacher", "Germany", 287, 91, new int[] { 1994, 1995, 2000, 2001, 2002, 2003, 2004 }, new string[] { "Benetton", "Ferrari" }),
        new("Fernando", "Alonso", "Spain", 314, 32, new int[] { 2005, 2006 }, new string[] { "Renault" }),
        new("Kimi", "Räikkönen", "Finland", 330, 21, new int[] { 2007 }, new string[] { "Ferrari" }),
        new("Lewis", "Hamilton", "UK", 266, 95, new int[] { 2008, 2014, 2015, 2017, 2018, 2019, 2020 }, new string[] { "McLaren", "Mercedes" }),
        new("Jenson", "Button", "UK", 306, 16, new int[] { 2009 }, new string[] { "Brawn GP" }),
        new("Sebastian", "Vettel", "Germany", 257, 53, new int[] { 2010, 2011, 2012, 2013 }, new string[] { "Red Bull Racing" }),
        new("Nico", "Rosberg", "Germany", 207, 24, new int[] { 2016 }, new string[] { "Mercedes" })
    };
}
