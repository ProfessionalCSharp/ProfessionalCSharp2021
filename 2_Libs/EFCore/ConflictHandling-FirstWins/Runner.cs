﻿namespace ConfictHandling;

public class Runner(BooksContext booksContext)
{
    private readonly BooksContext _booksContext = booksContext;
    private string? _user;
    private Book? _selectedBook;

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

    public async Task<int> PrepareUpdateAsync(string user, int id = 0)
    {
        _user = user;
        if (id is 0)
        {
            _selectedBook = await _booksContext.Books.OrderBy(b => b.BookId).LastAsync();
            return _selectedBook.BookId;
        }
        _selectedBook = await _booksContext.Books.FindAsync(id);
        return id;
    }

    public async Task UpdateAsync()
    {
        if (_selectedBook is null || _user is null) 
            throw new InvalidOperationException("_selectedBook not set. Invoke PrepareUpdateAsync before UpdateAsync");

        try
        {
            _selectedBook.Title = $"Book updated from {_user}";
            int records = await _booksContext.SaveChangesAsync();
            if (records == 1)
            {
                Console.WriteLine($"Book {_selectedBook.BookId} updated from {_user}");
            }
        }
        catch (DbUpdateConcurrencyException ex)
        {
            Console.WriteLine($"{_user}: update failed with {_selectedBook.Title}");
            Console.WriteLine($"error: {ex.Message}");
            foreach (var entry in ex.Entries)
            {
                if (entry.Entity is Book b)
                {
                    PropertyEntry pe = entry.Property("Timestamp");
                    Console.WriteLine($"{b.Title} {pe.TimestampToString()}");
                    ShowChanges(_selectedBook.BookId, _booksContext.Entry(_selectedBook));
                }
            }
        }
    }

    private static void ShowChanges(int id, EntityEntry entity)
    { 
        static void ShowChange(PropertyEntry propertyEntry, int id) =>
          Console.WriteLine($"id: {id}, current: {propertyEntry.CurrentValue}, " +
            $"original: {propertyEntry.OriginalValue}, " +
            $"modified: {propertyEntry.IsModified}");

        ShowChange(entity.Property("Title"), id);
        ShowChange(entity.Property("Publisher"), id);
    }

    public async Task<string> GetUpdatedTitleAsyc(int id)
    {
        var book = await _booksContext.Books.FindAsync(id);
        if (book is null) 
            return string.Empty;
        return $"{book.Title} with id {book.BookId}";
    }
}

internal static class Converter
{
    public static string TimestampToString(this PropertyEntry entry)
    {
        if (entry.CurrentValue is null) 
            return string.Empty;
        byte[] data = (byte[])entry.CurrentValue;
        return BitConverter.ToString(data);
    }
}
