using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
        Console.Write("Delete the database? ");
        string? input = Console.ReadLine();
        if (input?.ToLower() == "y")
        {
            bool deleted = await _booksContext.Database.EnsureDeletedAsync();
            string deletionInfo = deleted ? "deleted" : "not deleted";
            Console.WriteLine($"database {deletionInfo}");
        }
    }

    public async Task ApplyMigrationsAsync()
    {
        await _booksContext.Database.MigrateAsync();
    }

    async Task AddBookAsync(string title, string publisher)
    {
        Book book = new(Title: title, Publisher: publisher);
        await _booksContext.Books.AddAsync(book);
        int records = await _booksContext.SaveChangesAsync();
        Console.WriteLine($"{records} record added");

        Console.WriteLine();
    }

    public async Task AddBooksAsync()
    {
        Book b1 = new(Title: "Professional C# 6 and .NET Core 1.0", Publisher: "Wrox Press");
        Book b2 = new(Title: "Professional C# 5 and .NET 4.5.1", Publisher: "Wrox Press");
        Book b3 = new(Title: "JavaScript for Kids", Publisher: "Wrox Press");
        Book b4 = new(Title: "Web Design with HTML and CSS", Publisher: "For Dummies");
        await _booksContext.Books.AddRangeAsync(b1, b2, b3, b4);
        int records = await _booksContext.SaveChangesAsync();
        Console.WriteLine($"{records} records added");

        Console.WriteLine();
    }

    public async Task ReadBooksAsync(CancellationToken cancel = default)
    {
        List<Book> books = await _booksContext.Books.ToListAsync(cancel);
        foreach (var b in books)
        {
            Console.WriteLine($"{b.Title} {b.Publisher}");
        }

        Console.WriteLine();
    }

    public async Task QueryBooksAsync(CancellationToken token = default)
    {
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
        int records = 0;
        Book book = await _booksContext.Books
            .Where(b => b.Title == "Professional C# 7")
            .FirstOrDefaultAsync();
        if (book != null)
        {
            Book bookUpdate = book with { Title = "Professional C# 7 and .NET Core 2" };
            _booksContext.Update(bookUpdate);
            records = await _booksContext.SaveChangesAsync();
        }
        Console.WriteLine($"{records} record updated");
        Console.WriteLine();
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
