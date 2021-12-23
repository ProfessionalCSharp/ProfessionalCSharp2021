using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace StylesAndResources;

public sealed partial class ResourcesDemoUserControl : UserControl
{
    public ResourcesDemoUserControl() => InitializeComponent();

    private void OnApplyResources(object sender, RoutedEventArgs e)
    {
        var o = Application.Current.Resources["MyGradientBrush"];
        if (sender is Control ctrl)
        {
            if (ctrl.Background is null)
            {
                var brush = Application.Current.Resources["MyGradientBrush"] as Brush;
                ctrl.Background = brush;
            }
            else
            {
                ctrl.Background = null;
            }
        }
    }
}