using Microsoft.EntityFrameworkCore;

namespace TemporalTableSample;

public class Runner
{
    private readonly BooksContext _booksContext;
    public Runner(BooksContext booksContext)
    {
        _booksContext = booksContext;
    }

    public async Task CreateTheDatabaseAsync()
    {
        bool created = await _booksContext.Database.EnsureCreatedAsync();
        string creationInfo = created ? "created" : "exists";
        Console.WriteLine($"database {creationInfo}");
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

    public async Task AddBookAsync(string title, string publisher)
    {
        Book book = new(title, publisher);
        await _booksContext.Books.AddAsync(book);
        int records = await _booksContext.SaveChangesAsync();
        Console.WriteLine($"{records} record added with id {book.BookId}");

        Console.WriteLine();
    }

    public async Task AddBooksAsync()
    {
        Book b1 = new("Professional C# 7 and .NET Core 2", "Wrox Press");
        Book b2 = new("Professional C# 6 and .NET Core 1.0", "Wrox Press");
        Book b3 = new("Professional C# 5 and .NET 4.5.1", "Wrox Press");
        Book b4 = new("Essential Algorithms", "Wiley");
        await _booksContext.Books.AddRangeAsync(b1, b2, b3, b4);
        int records = await _booksContext.SaveChangesAsync();
        Console.WriteLine($"{records} records added");

        Console.WriteLine();
    }

    public async Task ReadBooksAsync(CancellationToken token = default)
    {
#if DEBUG
        string query = _booksContext.Books.ToQueryString();
        Console.WriteLine(query);
#endif
        List<Book> books = await _booksContext.Books.ToListAsync(token);
        foreach (var b in books)
        {
            Console.WriteLine($"{b.Title} {b.Publisher}");
        }

        Console.WriteLine();
    }

    public async Task QueryBooksAsync(CancellationToken token = default)
    {
        string query = _booksContext.Books.Where(b => b.Publisher == "Wrox Press").ToQueryString();
        Console.WriteLine(query);
        await _booksContext.Books
            .Where(b => b.Publisher == "Wrox Press")
            .ForEachAsync(b =>
            {
                Console.WriteLine($"{b.Title} {b.Publisher}");
            }, token);

        Console.WriteLine();
    }

    public async Task UpdateBookAsync()
    {
        Book? book = await _booksContext.Books.FindAsync(1);
        Console.WriteLine("Just a short delay before updating...");
        await Task.Delay(TimeSpan.FromSeconds(5));

        if (book != null)
        {
            book.Title = "Professional C# and .NET - 2021 Edition";
            int records = await _booksContext.SaveChangesAsync();
            Console.WriteLine($"{records} record updated");
        }
        Console.WriteLine();
    }


    public async Task TemporalPointInTimeQueryAsync()
    {
        Book? book = await _booksContext.Books.FindAsync(1);
        if (book is null) return;
        // read shadow property for time
        if (_booksContext.Entry(book).CurrentValues["PeriodStart"] is DateTime periodStart)
        {
            DateTime previousTime = periodStart.AddSeconds(-4);
            var previousBook = await _booksContext.Books
                .TemporalAsOf(previousTime)
                .TagWith("temporalasof")
                .SingleOrDefaultAsync(b => b.BookId == book.BookId);

            Console.WriteLine($"actual: {book.BookId}: {book.Title}, {book.Publisher}");
            if (previousBook is not null)
            {
                Console.WriteLine($"earlier: {previousBook.BookId}: {previousBook.Title}, " +
                    $"{previousBook.Publisher}");
            }
        }
    }

    public async Task TemporalAllQueryAsync()
    {
        await _booksContext.Books
            .TemporalAll()
            .TagWith("temporalall")
            .ForEachAsync(b =>
        {
            var entry = _booksContext.Entry(b);
            Console.WriteLine($"{b.Title} {entry.State}");
        });
    }

    public async Task DeleteBooksAsync()
    {
        List<Book> books = await _booksContext.Books.ToListAsync();
        _booksContext.Books.RemoveRange(books);
        int records = await _booksContext.SaveChangesAsync();
        Console.WriteLine($"{records} records deleted");

        Console.WriteLine();
    }
}
