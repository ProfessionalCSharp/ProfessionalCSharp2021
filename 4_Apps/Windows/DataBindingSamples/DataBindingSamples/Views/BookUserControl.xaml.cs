using DataBindingSamples.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace DataBindingSamples.Views;

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
