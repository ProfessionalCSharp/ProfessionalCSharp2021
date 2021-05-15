// using Microsoft.AppCenter.Analytics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WindowsAppAnalytics
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            Title = "WinUI App with AppCenter Analytics";
            this.InitializeComponent();
            // Analytics.TrackEvent(EventNames.PageNavigation, new Dictionary<string, string> { ["Page"] = nameof(MainWindow) });
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            // Analytics.TrackEvent(EventNames.ButtonClicked, new Dictionary<string, string> { ["State"] = textState.Text });
        }

        private void OnAnalyticsChanged(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkbox)
            {
                bool isChecked = checkbox?.IsChecked ?? true;
                // await Analytics.SetEnabledAsync(isChecked);
            }
        }
    }
}
