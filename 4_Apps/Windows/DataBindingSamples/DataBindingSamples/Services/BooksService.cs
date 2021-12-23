using DataBindingSamples.Models;

using System.Collections.ObjectModel;

namespace DataBindingSamples.Services;

public class BooksService
{
    private readonly ObservableCollection<Book> _books = new();

    public void RefreshBooks()
    {
        _books.Clear();
        SampleBooksService sampleBooksService = new();
        var books = sampleBooksService.GetSampleBooks();
        foreach (var book in books)
        {
            _books.Add(book);
        }
    }

    public Book? GetBook(int bookId) =>
        _books.SingleOrDefault(b => b.BookId == bookId);

    public void AddBook(Book book) => _books.Add(book);

    public ObservableCollection<Book> Books => _books;
}
