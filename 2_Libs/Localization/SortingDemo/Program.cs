using System.Collections;
using System.Globalization;

string[] countries = { "Österreich", "België", "България", "Hrvatska", "Česko", "Danmark", "Eesti", "Suomi", "France",
    "Deutschland", "Ελλάδα", "Magyarország", "Ireland", "Italia", "Latvija", "Lietuva", "Lëtzebuerg", "Malta", "Nederland",
    "Polska", "Portugal", "România", "Slovensko", "Slovenija", "España", "Sverige" };

CultureInfo.CurrentCulture = new CultureInfo("fi-FI");

Array.Sort(countries);
DisplayNames("Sorted using the Finnish culture", countries);

// sort using the invariant culture

Array.Sort(countries, Comparer.DefaultInvariant);
DisplayNames("Sorted using the invariant culture", countries);

static void DisplayNames(string title, IEnumerable<string> names)
{
    Console.WriteLine(title);
    Console.WriteLine(string.Join("-", names));
    Console.WriteLine();
}
