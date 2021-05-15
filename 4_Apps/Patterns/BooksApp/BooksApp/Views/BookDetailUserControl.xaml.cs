using BooksLib.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BooksApp.Views
{
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
}
