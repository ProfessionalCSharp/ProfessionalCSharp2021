﻿using Microsoft.UI.Input;
using Microsoft.UI.Xaml.Input;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BooksApp.Views;

public sealed partial class BookItemUserControl : UserControl
{
    public BookItemUserControl()
    {
        InitializeComponent();
    }

    public BookItemViewModel BookItemViewModel
    {
        get => (BookItemViewModel)GetValue(BookItemViewModelProperty);
        set => SetValue(BookItemViewModelProperty, value);
    }

    public static readonly DependencyProperty BookItemViewModelProperty =
        DependencyProperty.Register("BookViewModel", typeof(BookItemViewModel), typeof(BookItemUserControl), new PropertyMetadata(null));

    protected override void OnPointerEntered(PointerRoutedEventArgs e)
    {
        base.OnPointerEntered(e);

        if (e.Pointer.PointerDeviceType == PointerDeviceType.Mouse || e.Pointer.PointerDeviceType == PointerDeviceType.Pen)
        {
            VisualStateManager.GoToState(this, "HoverButtonsShown", true);
        }
    }

    protected override void OnPointerExited(PointerRoutedEventArgs e)
    {
        base.OnPointerExited(e);
        VisualStateManager.GoToState(this, "HoverButtonsHidden", true);
    }
}
