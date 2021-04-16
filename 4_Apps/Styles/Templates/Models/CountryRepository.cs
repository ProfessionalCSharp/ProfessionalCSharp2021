using System.Collections.Generic;

namespace Models
{
    public sealed class CountryRepository
    {
        private static IEnumerable<Country>? s_countries;

        public IEnumerable<Country> GetCountries() => s_countries ??= new List<Country>
        {
            new() { Name = "Austria", ImagePath = "/Images/Austria.bmp" },
            new() { Name = "Germany", ImagePath = "/Images/Germany.bmp" },
            new() { Name = "Norway", ImagePath = "/Images/Norway.bmp" },
            new() { Name = "USA", ImagePath = "/Images/USA.bmp" }
        };
    }
}
