using BooksLib.Models;

using CommunityToolkit.Mvvm.Input;

using GenericViewModels.Services;
using GenericViewModels.ViewModels;

namespace BooksLib.ViewModels;

// this is the view model display book items within a list
public class BookItemViewModel : ItemViewModel<Book>
{
    private readonly IItemsService<Book> _booksService;

    public BookItemViewModel(Book book, IItemsService<Book> booksService)
        : base(book)
    {
        _booksService = booksService ?? throw new ArgumentNullException(nameof(booksService));
        DeleteBookCommand = new RelayCommand(OnDeleteBook);
    }

    public RelayCommand DeleteBookCommand { get; set; }

    private async void OnDeleteBook()
    {
        if (Item is null) return;

        await _booksService.DeleteAsync(Item);
    }
}
