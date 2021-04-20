using DataBindingSamples.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DataBindingSamples.Views
{
    public sealed partial class BookUserControl : UserControl
    {
        public BookUserControl() => InitializeComponent();

        public Book? Book
        {
            get => (Book)GetValue(BookProperty);
            set => SetValue(BookProperty, value);
        }

        public static readonly DependencyProperty BookProperty =
            DependencyProperty.Register("Book", typeof(Book), typeof(BookUserControl), new PropertyMetadata(null));
    }
}
