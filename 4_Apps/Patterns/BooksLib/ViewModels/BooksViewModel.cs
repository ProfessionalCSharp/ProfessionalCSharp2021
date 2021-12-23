using BooksLib.Models;
using BooksLib.Services;

using GenericViewModels.Services;
using GenericViewModels.ViewModels;

namespace BooksLib.ViewModels;

public class BooksViewModel : MasterDetailViewModel<BookItemViewModel, Book>
{
    private readonly IItemsService<Book> _booksService;
    private readonly INavigationService _navigationService;

    public BooksViewModel(IItemsService<Book> booksService, INavigationService navigationService)
        : base(booksService)
    {
        _booksService = booksService ?? throw new ArgumentNullException(nameof(booksService));
        _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));

        PropertyChanged += async (sender, e) =>
        {
            if (_navigationService.UseNavigation && e.PropertyName == nameof(SelectedItem) && _navigationService.CurrentPage == PageNames.BooksPage)
            {
                await _navigationService.NavigateToAsync(PageNames.BookDetailPage);
            }
        };
    }

    public override void OnAdd()
    {
        Book newBook = new();
        Items.Add(newBook);
        SelectedItem = newBook;
    }

    protected override BookItemViewModel ToViewModel(Book item) => new(item, _booksService);
}
