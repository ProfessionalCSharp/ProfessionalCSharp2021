using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

public class Runner
{
    private readonly BooksContext _booksContext;

    public Runner(BooksContext booksContext) => _booksContext = booksContext;

    public async Task CreateTheDatabaseAsync()
    {
        bool created = await _booksContext.Database.EnsureCreatedAsync();
        string creationInfo = created ? "created" : "exists";
        Console.WriteLine($"database {creationInfo}");
    }

    public async Task EagerLoadingAsync()
    {
        Console.WriteLine(nameof(EagerLoadingAsync));
        var books = await _booksContext.Books
            .Where(b => b.Publisher == "pub1")
            .Include(b => b.Author)
            .ThenInclude(a => a!.Address)
            .Include(b => b.Chapters)
            .ToListAsync();
        foreach (var book in books)
        {
            Console.WriteLine($"{book.Title} {book.Author?.FirstName} {book.Author?.Address?.Country}");
        }
        Console.WriteLine();
    }

    public async Task FilteredIncludeAsync()
    {
        Console.WriteLine(nameof(FilteredIncludeAsync));
        var books = await _booksContext.Books
            .Where(b => b.Publisher == "pub2")
            .Include(b => b.Author)
            .ThenInclude(a => a!.Address)
            .Include(b => b.Chapters!.Where(c => c.ChapterId > 5)) 
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
            bool deleted = await _booksContext.Database.EnsureDeletedAsync();
            string deletionInfo = deleted ? "deleted" : "not deleted";
            Console.WriteLine($"database {deletionInfo}");
        }
    }

    public async Task ExplicitLoadingAsync()
    {
        Console.WriteLine(nameof(ExplicitLoadingAsync));
        var books = await _booksContext.Books
            .Where(b => b.Publisher == "pub1")
            .ToListAsync();

        foreach (var book in books)
        {
            Console.WriteLine(book.Title);
            var bookEntry = _booksContext.Entry(book);
            await bookEntry.Reference(b => b.Author).LoadAsync();
            Console.WriteLine($"{book.Author?.FirstName} {book.Author?.LastName}");

            if (book.Author is null) continue; // no author, fix CS8634 warning
            await _booksContext.Entry(book.Author).Reference(a => a.Address).LoadAsync();
            Console.WriteLine($"{book.Author!.Address!.Country}");

            await bookEntry.Collection(b => b.Chapters).LoadAsync();

            foreach (var chapter in book.Chapters)
            {
                Console.WriteLine(chapter.Title);
            }
        }
        Console.WriteLine();
    }

    public async Task LazyLoadingAsync()
    {
        Console.WriteLine(nameof(LazyLoadingAsync));
        var books = await _booksContext.Books
            .Where(b => b.Publisher == "pub1")
            .ToListAsync();

        foreach (var book in books)
        {
            Console.WriteLine(book.Title);
            Console.WriteLine($"{book.Author?.FirstName} {book.Author?.LastName}");

            if (book.Author is null)
            {
                Console.WriteLine("configure the proxy for lazy loading");
                return;
            }
            Console.WriteLine($"{book.Author!.Address!.Country}");
            foreach (var chapter in book.Chapters)
            {
                Console.WriteLine(chapter.Title);
            }
        }
        Console.WriteLine();
    }
}
