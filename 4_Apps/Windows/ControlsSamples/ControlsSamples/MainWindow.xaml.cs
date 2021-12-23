using ControlsSamples.Views;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;

namespace ControlsSamples;

public sealed partial class MainWindow : Window
{
    public MainWindow() => InitializeComponent();

    private void OnNavigate(XamlUICommand sender, ExecuteRequestedEventArgs args)
    {
        Type pageType = args.Parameter switch
        {
            "Text" => typeof(TextPage),
            "Presenters" => typeof(PresentersPage),
            "Ranges" => typeof(RangeControlsPage),
            "Buttons" => typeof(ButtonsPage),
            "Dates" => typeof(DateSelectionPage),
            "Web" => typeof(WebView2Page),
            _ => throw new InvalidOperationException()
        };

        MainFrame.Navigate(pageType);
    }
}
