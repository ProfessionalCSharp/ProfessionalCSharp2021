namespace AutoInclude;

public class Runner(BooksContext booksContext)
{
    public async Task CreateTheDatabaseAsync()
    {
        bool created = await booksContext.Database.EnsureCreatedAsync();
        string creationInfo = created ? "created" : "exists";
        Console.WriteLine($"database {creationInfo}");
    }

    public async Task AutoIncludeLoading()
    {
        Console.WriteLine(nameof(AutoIncludeLoading));
        var books = await booksContext.Books
            .Where(b => b.Publisher == "pub1")
            .ToListAsync();
        foreach (var book in books)
        {
            Console.WriteLine($"{book.Title} {book.Author?.FirstName} {book.Author?.Address?.Country}");
        }
        Console.WriteLine();
    }

    public async Task DeleteDatabaseAsync()
    {
        Console.Write("Delete the database? (y|n) ");
        string? input = Console.ReadLine();
        if (input?.ToLower() == "y")
        {
            bool deleted = await booksContext.Database.EnsureDeletedAsync();
            string deletionInfo = deleted ? "deleted" : "not deleted";
            Console.WriteLine($"database {deletionInfo}");
        }
    }
}
