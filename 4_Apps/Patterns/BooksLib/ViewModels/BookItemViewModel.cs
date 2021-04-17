using BooksLib.Models;
using GenericViewModels.Services;
using GenericViewModels.ViewModels;
using Microsoft.Toolkit.Mvvm.Input;
using System;

namespace BooksLib.ViewModels
{
    // this is the view model display book items within a list
    public class BookItemViewModel : ItemViewModel<Book>
    {
        private readonly IItemsService<Book> _booksService;

        public BookItemViewModel(Book book, IItemsService<Book> booksService)
        {
            Item = book;
            _booksService = booksService ?? throw new ArgumentNullException(nameof(booksService));
            DeleteBookCommand = new RelayCommand(OnDeleteBook);
        }

        public RelayCommand DeleteBookCommand { get; set; }

        private async void OnDeleteBook()
        {
            await _booksService.DeleteAsync(Item);
        }
    }
}
