using LayoutSamples.Views;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;

namespace LayoutSamples;

public sealed partial class MainWindow : Window
{
    public MainWindow() => InitializeComponent();

    private void OnNavigate(XamlUICommand sender, ExecuteRequestedEventArgs args)
    {
        Type pageType = args.Parameter switch
        {
            "Adaptive" => typeof(AdaptiveRelativePanelPage),
            "Delay" => typeof(DelayLoadingPage),
            "Grid" => typeof(GridPage),
            "Relative" => typeof(RelativePanelPage),
            "Stack" => typeof(StackPanelPage),
            "Variable" => typeof(VariableSizedWrapGridPage),
            _ => throw new InvalidOperationException()
        };

        MainFrame.Navigate(pageType);
    }
}
