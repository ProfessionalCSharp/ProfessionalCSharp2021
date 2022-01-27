using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;

using NavigationControls.Views;

namespace NavigationControls;

public sealed partial class MainWindow : Window
{
    public MainWindow() => InitializeComponent();

    private void OnNavigate(XamlUICommand sender, ExecuteRequestedEventArgs args)
    {
        Type pageType = args.Parameter switch
        {
            "Hub" => typeof(HubPage),
            "Tab" => typeof(TabViewPage),
            "Navigation" => typeof(NavigationViewPage),
            _ => throw new InvalidOperationException()
        };

        MainFrame.Navigate(pageType);
    }
}
