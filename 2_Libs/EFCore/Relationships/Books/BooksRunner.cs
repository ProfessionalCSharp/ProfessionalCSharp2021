public class BooksRunner
{
    private readonly BooksContext _booksContext;

    public BooksRunner(BooksContext booksContext) => _booksContext = booksContext;

    public async Task CreateTheDatabaseAsync()
    {
        var migrations = await _booksContext.Database.GetAppliedMigrationsAsync();
        if (!migrations.Any(s => s.Contains("InitBooks")))
        {
            await _booksContext.Database.MigrateAsync();
            Person[] authors = GetAuthors();
            _booksContext.People.AddRange(authors);
            Book[] books = GetBooks();
            AddAuthorsToBooks(books, authors);
            _booksContext.Books.AddRange(books);
            await _booksContext.SaveChangesAsync();
        }
    }

    private void AddAuthorsToBooks(Book[] books, Person[] authors)
    {
        int[][] bookAuthors =
        {
            new int[] { 0, 1, 2, 3, 4, 5, 6 },
            new int[] { 0, 1, 2, 3, 4, 6, 7, 8, 9 },
            new int[] { 0, 1, 6, 9, 10 },
            new int[] { 1, 3, 6, 9, 10, 11 },
            new int[] { 1, 3, 6, 9, 10 },
            new int[] { 1, 3, 6, 9, 10 },
            new int[] { 1, 3, 6, 9, 10 },
            new int[] { 1, 3, 9 },
            new int[] { 1, 3, 9 },
            new int[] { 1 },
            new int[] { 1 },
            new int[] { 1 },
        };
        
        for (int i = 0; i < books.Length; i++)
        {
            for (int j = 0; j < bookAuthors[i].Length; j++)
            {
                books[i].Authors.Add(authors[j]);
            }
        }
    }

    private Person[] GetAuthors() => new Person[]
        {
            new("Simon", "Robinson"),
            new("Christian", "Nagel"),
            new("Ollie", "Cornes"),
            new("Jay", "Glynn"),
            new("Burton", "Harvey"),
            new("Craig", "McQueen"),
            new("Karli", "Watson"),
            new("K S", "Allen"),
            new("Zach", "Greenvoss"),
            new("Morgan", "Skinner"),
            new("Bill", "Evjen"),
            new("Allen", "Jones"),
        };

    private Book[] GetBooks()
    {
        string[] titles =
        {
            "Professional C#", // CN, Simon Robinson, Ollie Cornes, Jay Glynn,  Burton Harvey, Craig McQueen, Karli Watson 
            "Professional C# 2nd Edition", // CN, Simon Robinson, K S Allen, Ollie Cornes, Jay Glynn, Zach Greenvoss, Burton Harvey, Morgan Skinner, Karli Watson
            "Professional C# 3rd Edition", // CN, Simon Robinson, Karli Watson, Morgan Skinner, Bill Evjen
            "Professional C# 2005", // CN, Bill Evjen, Jay Glynn, Karli Watson, Morgan Skinner, Allen Jones
            "Professional C# 2005 with .NET 3.0", // CN, Bill Evjen, Jay Glynn, Karli Watson, Morgan Skinner
            "Professional C# 2008", // CN, Bill Evjen, Karli Watson, Morgan Skinner, Jay Glynn
            "Professional C# 4 and .NET 4", // CN, Bill Evjen, Jay Glynn, Karli Watson, Morgan Skinner
            "Professional C# 2012 and .NET 4.5", // CN, Jay Glynn, Morgan Skinner
            "Professional C# 5.0 and .NET 4.5.1", // CN, Jay Glynn, Morgan Skinner
            "Professional C# 6 and .NET Core 1.0", // CN
            "Professional C# 7 and .NET Core 2.0", // CN
            "Professional C# and .NET 2021 Edition"  // CN
        };

        DateTime?[] dates =
        {
            new DateTime(2001, 6, 1), // .NET 1 Beta 2
            new DateTime(2003, 2, 28), // .NET 1
            new DateTime(2004, 6, 4), // .NET 1.1
            new DateTime(2005, 11, 4), // .NET 2
            new DateTime(2007, 3, 15), // .NET 3.0
            new DateTime(2008, 3, 28), // .NET 3.5
            new DateTime(2010, 3, 5), // C# 4, .NET 4
            new DateTime(2012, 10, 30), // .NET 4.5
            new DateTime(2014, 2, 14), // C# 5
            new DateTime(2016, 4, 29), // C# 6, .NET Core 1
            new DateTime(2018, 5, 29), // C# 7, .NET Core 2.1
            new DateTime(2021, 8, 31), // C# 9, .NET 5
        };

        List <Book> books = new();
        for (int i = 0; i < titles.Length; i++)
        {
            books.Add(new Book(titles[i], "Wrox Press") { ReleaseDate = dates[i] });
        }
        return books.ToArray();
    }

    public async Task GetBooksForAuthorAsync()
    {
        var books = await _booksContext.Books
            .Where(b => b.Title.StartsWith("Professional C#"))
            .Include(b => b.Authors)
            .ToListAsync();
        foreach (var b in books)
        {
            Console.WriteLine(b.Title);
            foreach (var a in b.Authors)
            {
                Console.Write($"{a.FirstName} {a.LastName}");
            }
            Console.WriteLine();
        }
    }

    public async Task DeleteDatabaseAsync()
    {
        Console.Write("Delete the database? (y|n) ");
        string? input = Console.ReadLine();
        if (input?.ToLower() == "y")
        {
            bool deleted = await _booksContext.Database.EnsureDeletedAsync();
            string deletionInfo = deleted ? "deleted" : "not deleted";
            Console.WriteLine($"database {deletionInfo}");
        }
    }
}
