using BlazorUnitedSample.Models;

namespace BlazorUnitedSample.Services;
public interface IBooksService
{
    Task<Book> AddBookAsync(Book book);
    Task<Book?> GetBookByIdAsync(int id);
    Task<IEnumerable<Book>> GetBooksAsync();
    Task<int> UpdateBookAsync(int id, Book book);
}