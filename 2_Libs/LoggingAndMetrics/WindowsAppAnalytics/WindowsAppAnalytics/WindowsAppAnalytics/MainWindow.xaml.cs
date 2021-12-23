using Microsoft.AppCenter.Analytics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace WindowsAppAnalytics;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        Title = "WinUI App with AppCenter Analytics";
        this.InitializeComponent();
        Analytics.TrackEvent(EventNames.PageNavigation, new Dictionary<string, string> { ["Page"] = nameof(MainWindow) });
    }

    private void OnButtonClick(object sender, RoutedEventArgs e)
    {
        Analytics.TrackEvent(EventNames.ButtonClicked, new Dictionary<string, string> { ["State"] = textState.Text });
    }

    private async void OnAnalyticsChanged(object sender, RoutedEventArgs e)
    {
        if (sender is CheckBox checkbox)
        {
            bool isChecked = checkbox?.IsChecked ?? true;
            await Analytics.SetEnabledAsync(isChecked);
        }
    }
}
