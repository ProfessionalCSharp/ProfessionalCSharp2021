using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;

namespace RoutedEvents;

public sealed partial class MainWindow : Window
{
    public MainWindow() => InitializeComponent();

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
