using BooksLib.Models;
using BooksLib.Services;

using GenericViewModels.Services;
using GenericViewModels.ViewModels;

using Microsoft.Extensions.Logging;

namespace BooksLib.ViewModels;

// this view model is used to display details of a book and allows editing
public class BookDetailViewModel : EditableItemViewModel<Book>
{
    private readonly IItemsService<Book> _itemsService;
    private readonly INavigationService _navigationService;
    private readonly IDialogService _dialogService;
    private readonly ILogger _logger;
    public BookDetailViewModel(IItemsService<Book> itemsService, INavigationService navigationService, IDialogService dialogService, ILogger<BookDetailViewModel> logger)
        : base(itemsService)
    {
        _itemsService = itemsService ?? throw new ArgumentNullException(nameof(itemsService));
        _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
        _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
        _logger = logger;

        itemsService.SelectedItemChanged += (sender, book) =>
        {
            Item = book;
        };
    }

    protected override void OnAdd()
    {
    }

    public override Book CreateCopy(Book? item)
    {
        int id = item?.BookId ?? -1;
        string title = item?.Title ?? "enter a title";
        string publisher = item?.Publisher ?? "enter a publisher";
        return new Book(title, publisher, id);
    }

    public override async Task OnSaveAsync()
    {
        try
        {
            if (EditItem is null) throw new InvalidOperationException();

            await _dialogService.ShowMessageAsync("TEst save");

            await _itemsService.AddOrUpdateAsync(EditItem);
        }
        catch (Exception ex)
        {
            _logger.LogError("error {0} in {1}", ex.Message, nameof(OnSaveAsync));
            await _dialogService.ShowMessageAsync("Error saving the data");
        }
    }

    public override async Task OnEndEditAsync()
    {
        if (_navigationService.UseNavigation)
        {
            await _navigationService.GoBackAsync();
        }
    }
}
