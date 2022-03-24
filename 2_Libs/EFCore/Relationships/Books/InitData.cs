internal class InitData
{
    public InitData()
    {
        _books = InitializeBooks();
        _authors = InitializeAuthors();
    }
    private Person[] _authors;
    private Book[] _books;

    private Book[] InitializeBooks()
    {
        string[] titles =
        {
            "Professional C#", // CN, Simon Robinson, Ollie Cornes, Jay Glynn,  Burton Harvey, Craig McQueen, Karli Watson 01-Jun-2001
            "Professional C# 2nd Edition", // CN, Simon Robinson, K S Allen, Ollie Cornes, Jay Glynn, Zach Greenvoss, Burton Harvey, Morgan Skinner, Karli Watson
            "Professional C# 3rd Edition", // CN, Simon Robinson, Karli Watson, Morgan Skinner, Bill Evjen, 4-jun-2004
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
        return Enumerable.Range(0, 12).Select(i => new Book(titles[i], "Wrox Press", i + 1) { ReleaseDate = dates[i] }).ToArray();
    }

    public IList<Book> GetBooks() => _books;
    public IList<Person> GetAuthors() => _authors;

    public object[] GetBooksAuthors()
        => new object[] 
        {
            new { WrittenBooksBookId = 1, AuthorsPersonId = 4 },
            new { WrittenBooksBookId = 1, AuthorsPersonId = 10 },
            new { WrittenBooksBookId = 1, AuthorsPersonId = 9 },
            new { WrittenBooksBookId = 1, AuthorsPersonId = 5 },
            new { WrittenBooksBookId = 1, AuthorsPersonId = 3 },
            new { WrittenBooksBookId = 1, AuthorsPersonId = 12 },
            new { WrittenBooksBookId = 1, AuthorsPersonId = 6 },
            new { WrittenBooksBookId = 2, AuthorsPersonId = 4 },
            new { WrittenBooksBookId = 2, AuthorsPersonId = 10 },
            new { WrittenBooksBookId = 2, AuthorsPersonId = 7 },
            new { WrittenBooksBookId = 2, AuthorsPersonId = 9 },
            new { WrittenBooksBookId = 2, AuthorsPersonId = 5 },
            new { WrittenBooksBookId = 2, AuthorsPersonId = 11 },
            new { WrittenBooksBookId = 2, AuthorsPersonId = 3 },
            new { WrittenBooksBookId = 2, AuthorsPersonId = 8 },
            new { WrittenBooksBookId = 2, AuthorsPersonId = 6 },
            new { WrittenBooksBookId = 3, AuthorsPersonId = 4 },
            new { WrittenBooksBookId = 3, AuthorsPersonId = 10 },
            new { WrittenBooksBookId = 3, AuthorsPersonId = 6 },
            new { WrittenBooksBookId = 3, AuthorsPersonId = 8 },
            new { WrittenBooksBookId = 3, AuthorsPersonId = 2 },
            new { WrittenBooksBookId = 4, AuthorsPersonId = 4 },
            new { WrittenBooksBookId = 4, AuthorsPersonId = 2 },
            new { WrittenBooksBookId = 4, AuthorsPersonId = 5 },
            new { WrittenBooksBookId = 4, AuthorsPersonId = 6 },
            new { WrittenBooksBookId = 4, AuthorsPersonId = 8 },
            new { WrittenBooksBookId = 4, AuthorsPersonId = 1 },
            new { WrittenBooksBookId = 5, AuthorsPersonId = 4 },
            new { WrittenBooksBookId = 5, AuthorsPersonId = 2 }
        };

    private Person[] InitializeAuthors()
    {
        var authors = new Person[]
        {
            new ("Allen", "Jones", 1),
            new ("Bill", "Evjen", 2),
            new ("Burton", "Harvey", 3),
            new ("Christian", "Nagel", 4),
            new ("Jay", "Glynn", 5),
            new ("Karli", "Watson", 6),
            new ("K S", "Allen", 7),
            new ("Morgan", "Skinner", 8),
            new ("Ollie", "Cornes", 9),
            new ("Simon", "Robinson", 10),
            new ("Zach", "Greenvoss", 11),
            new ("Craig", "McQueen", 12)
        };
        return authors;
    }
}

