using BooksLib.ViewModels;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BooksApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BookDetailPage : Page
    {
        public BookDetailPage() => InitializeComponent();

        public BookDetailViewModel ViewModel { get; } = Ioc.Default.GetRequiredService<BookDetailViewModel>();
    }
}
