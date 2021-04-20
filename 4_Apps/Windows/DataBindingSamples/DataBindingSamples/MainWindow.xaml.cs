using DataBindingSamples.Models;
using DataBindingSamples.Services;
using Microsoft.UI.Xaml;
using System.Collections.ObjectModel;
using System.Linq;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DataBindingSamples
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private BooksService _booksService = new();
        public MainWindow() => InitializeComponent();

        public ObservableCollection<Book> Books => _booksService.Books;

        public void OnRefreshBooks() => _booksService.RefreshBooks();

        public void OnAddBook() =>
          _booksService.AddBook(new Book(GetNextBookId(),
            $"Professional C# {GetNextBookId() + 4}", "Wrox Press"));

        private int GetNextBookId() => Books.Select(b => b.BookId).Max() + 1;
    }
}
