using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RoutedEvents
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }
        private void OnTappedButton(object sender, TappedRoutedEventArgs e)
        {
            ShowStatus(nameof(OnTappedButton), e);
            e.Handled = CheckStopRouting.IsChecked == true;
        }

        private void OnTappedGrid(object sender, TappedRoutedEventArgs e)
        {
            ShowStatus(nameof(OnTappedGrid), e);
            e.Handled = CheckStopRouting.IsChecked == true;
        }

        private void ShowStatus(string status, RoutedEventArgs e)
        {
            textStatus.Text += $"{status} {e.OriginalSource.GetType().Name}";
            textStatus.Text += "\r\n";
        }

        private void OnCleanStatus(object sender, RoutedEventArgs e)
        {
            textStatus.Text = string.Empty;
        }
    }
}
