namespace BooksApp.ViewModels;

public class MainWindowViewModel(INavigationService navigationService, WinUIInitializeNavigationService initializeNavigationService) : ViewModelBase
{
    private readonly Dictionary<string, Type> _pages = new()
    {
        [PageNames.BooksPage] = typeof(BooksPage),
        [PageNames.BookDetailPage] = typeof(BookDetailPage)
    };

    public void SetNavigationFrame(Frame frame) => initializeNavigationService.Initialize(frame, _pages);

    public void UseNavigation(bool navigation)
    {
        navigationService.UseNavigation = navigation;
    }

    public void OnNavigationSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItem is NavigationViewItem navigationItem)
        {
            switch (navigationItem.Tag)
            {
                case "books":
                    navigationService.NavigateToAsync(PageNames.BooksPage);
                    break;
                default:
                    break;
            }
        }
    }
}
