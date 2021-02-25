using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

public class BooksRunner
{
    private readonly BooksContext _booksContext;

    public BooksRunner(BooksContext booksContext) => _booksContext = booksContext;

    public async Task CreateTheDatabaseAsync()
    {
        await _booksContext.Database.MigrateAsync();
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
