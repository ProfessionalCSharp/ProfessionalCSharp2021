using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Transitions.Views;

public sealed partial class PaneTransitionUserControl : UserControl
{
    public PaneTransitionUserControl() => InitializeComponent();

    private void OnShow(object sender, RoutedEventArgs e)
    {
        popup1.IsOpen = true;
        popup2.IsOpen = true;
        popup3.IsOpen = true;
    }

    private void OnHide(object sender, RoutedEventArgs e)
    {
        popup1.IsOpen = false;
        popup2.IsOpen = false;
        popup3.IsOpen = false;
    }
}
