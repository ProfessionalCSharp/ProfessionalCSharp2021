using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace StylesAndResources;

public sealed partial class UseThemesUserControl : UserControl
{
    public UseThemesUserControl() => InitializeComponent();

    private void OnChangeTheme(object sender, RoutedEventArgs e)
    {
        grid1.RequestedTheme = grid1.RequestedTheme == ElementTheme.Dark ?
            ElementTheme.Light : ElementTheme.Dark;
    }
}
