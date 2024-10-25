using BlazorUnitedSample.Models;
using BlazorUnitedSample.Services;

using Microsoft.EntityFrameworkCore;

namespace BlazorUnitedSample.Data;

public class BooksContext(DbContextOptions<BooksContext> options) : DbContext(options), IBooksService
{
    public DbSet<Book> Book => Set<Book>();

    public async Task<IEnumerable<Book>> GetBooksAsync()
    {
        return await Book.ToListAsync();
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
        return await Book.AsNoTracking()
            .FirstOrDefaultAsync(model => model.Id == id);
    }

    public async Task<int> UpdateBookAsync(int id, Book book)
    {
        return await Book
            .Where(model => model.Id == id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(m => m.Id, book.Id)
                .SetProperty(m => m.Title, book.Title)
                .SetProperty(m => m.Publisher, book.Publisher)
                );
    }

    public async Task<Book> AddBookAsync(Book book)
    {
        Book.Add(book);
        await SaveChangesAsync();

        return book;
    }
}
