using DataBindingSamples.Models;
using DataBindingSamples.Services;

using Microsoft.UI.Xaml;

using System.Collections.ObjectModel;

namespace DataBindingSamples;
public sealed partial class MainWindow : Window
{
    private readonly BooksService _booksService = new();
    public MainWindow() => InitializeComponent();

    public ObservableCollection<Book> Books => _booksService.Books;

    public void RefreshBooks() => _booksService.RefreshBooks();

    public void AddBook() =>
      _booksService.AddBook(new Book(GetNextBookId(),
        $"Professional C# and .NET - {GetNextYear()} Edition", "Wrox Press"));

    private int GetNextBookId() => Books.Select(b => b.BookId).Max() + 1;
    private int _year = 2021;
    private int GetNextYear() => _year += 3;
}
