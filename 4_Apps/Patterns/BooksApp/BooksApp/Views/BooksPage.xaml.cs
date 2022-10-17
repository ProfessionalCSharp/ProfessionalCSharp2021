using CommunityToolkit.Mvvm.DependencyInjection;

namespace BooksApp.Views;

public sealed partial class BooksPage : Page
{
    public BooksPage()
    {
        InitializeComponent();
        BookDetailUC.ViewModel = Ioc.Default.GetRequiredService<BookDetailViewModel>();
    }

    public BooksViewModel ViewModel { get; } = Ioc.Default.GetRequiredService<BooksViewModel>();

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
    }
}
