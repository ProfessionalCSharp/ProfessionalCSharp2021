using DataBindingSamples.Models;
using DataBindingSamples.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

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
        public MainWindow()
        {
            this.InitializeComponent();
        }

        public ObservableCollection<Book> Books => _booksService.Books;

        public void OnRefreshBooks()
        {
            _booksService.RefreshBooks();
        }

        public void OnAddBook() =>
          _booksService.AddBook(new Book(GetNextBookId(),
            $"Professional C# {GetNextBookId() + 3}", "Wrox Press"));

        private int GetNextBookId() => Books.Select(b => b.BookId).Max() + 1;
    }
}
