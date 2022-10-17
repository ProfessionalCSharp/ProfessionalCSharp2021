using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace LayoutSamples.Views;

public sealed partial class DelayLoadingPage : Page
{
    public DelayLoadingPage() => InitializeComponent();

    private void OnDeferLoad(object sender, RoutedEventArgs e)
    {
        FindName(nameof(deferGrid));
    }
}
