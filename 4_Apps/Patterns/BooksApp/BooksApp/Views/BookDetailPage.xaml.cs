namespace BooksApp.Views;

public sealed partial class BookDetailPage : Page
{
    public BookDetailPage() => InitializeComponent();

    public BookDetailViewModel ViewModel { get; } = Ioc.Default.GetRequiredService<BookDetailViewModel>();
}
