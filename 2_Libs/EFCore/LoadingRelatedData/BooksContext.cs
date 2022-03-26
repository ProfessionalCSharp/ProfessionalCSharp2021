using Microsoft.EntityFrameworkCore;

public class BooksContext : DbContext
{
    public BooksContext(DbContextOptions<BooksContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>().HasData(GetSampleAddresses());
        modelBuilder.Entity<Person>().HasData(GetSamplePeople());
        modelBuilder.Entity<Chapter>().HasData(GetSampleChapters());
        modelBuilder.Entity<Book>().HasData(GetSampleBooks());
        base.OnModelCreating(modelBuilder);
    }

    private readonly Random _random = new Random();

    private IEnumerable<Book> GetSampleBooks()
        => Enumerable.Range(1, 100)
        .Select(id => new Book($"title {id}", "sample", id) { Publisher = _publishers[_random.Next(0, 2)], AuthorId = _random.Next(1, 3) })
        .ToArray();

    private IEnumerable<Chapter> GetSampleChapters()
        => Enumerable.Range(1, 100)
        .Select(id => new Chapter($"chapter {id % 10}", id) { BookId = (id / 10 + 1) })
        .ToArray();

    private readonly string[] _publishers = { "pub1", "pub2", "pub3" };
    private readonly string[] _countries = { "country1", "country2", "country3" };
    private readonly string[] _cities = { "city1", "city2", "city3" };
    private readonly string[] _firstNames = { "first1", "first2", "first3" };
    private readonly string[] _lastNames = {"last1", "last2", "last3"};
    private IEnumerable<Address> GetSampleAddresses()
        => Enumerable.Range(1, 3)
        .Select(i => new Address { AddressId = i, Country = _countries[i - 1], City = _cities[i - 1]})
        .ToArray();

    private IEnumerable<Person> GetSamplePeople()
        => Enumerable.Range(1, 3)
        .Select(i => new Person(_firstNames[i - 1], _lastNames[i - 1], i) { AddressId = i })
        .ToArray();

    public DbSet<Book> Books => Set<Book>();
    public DbSet<Chapter> Chapters => Set<Chapter>();
    public DbSet<Person> People => Set<Person>();
    public DbSet<Address> Addresses => Set<Address>();
}
