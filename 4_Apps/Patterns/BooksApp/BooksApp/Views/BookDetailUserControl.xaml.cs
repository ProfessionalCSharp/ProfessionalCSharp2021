namespace BooksApp.Views;

public sealed partial class BookDetailUserControl : UserControl
{
    public BookDetailUserControl()
    {
        this.InitializeComponent();
    }

    public BookDetailViewModel ViewModel
    {
        get => (BookDetailViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register("ViewModel", typeof(BookDetailViewModel), typeof(BookDetailUserControl), new PropertyMetadata(null));
}
