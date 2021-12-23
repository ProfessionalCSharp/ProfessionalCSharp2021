using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Transitions.Views;

public sealed partial class RepositionUserControl : UserControl
{
    public RepositionUserControl() => InitializeComponent();

    private void OnReposition(object sender, RoutedEventArgs e)
    {
        buttonReposition.Margin = new Thickness(100);
        button2.Margin = new Thickness(100);
    }

    private void OnReset(object sender, RoutedEventArgs e)
    {
        buttonReposition.Margin = new Thickness(10);
        button2.Margin = new Thickness(10);
    }
}
