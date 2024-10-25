using BlazorUnitedSample.Models;
using BlazorUnitedSample.Services;

using System.Net.Http.Json;

namespace BlazorUnitedSample.Client.Services;

public class BooksAPIClient(HttpClient client) : IBooksService
{
    public async Task<Book> AddBookAsync(Book book)
    {
        await client.PostAsJsonAsync("/api/Book", book);
        return book;
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
        var book = await client.GetFromJsonAsync<Book>($"/api/Book/{id}");
        return book;
    }

    public async Task<IEnumerable<Book>> GetBooksAsync()
    {
        var books = await client.GetFromJsonAsync<IEnumerable<Book>>("/api/Book");
        return books ?? [];
    }

    public Task<int> UpdateBookAsync(int id, Book book)
    {
        throw new NotImplementedException();
    }
}
