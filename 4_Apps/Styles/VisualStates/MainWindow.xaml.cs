using Microsoft.UI.Xaml;

namespace VisualStates;

public sealed partial class MainWindow : Window
{
    public MainWindow() => InitializeComponent();

    private void OnEnable(object sender, RoutedEventArgs e) =>
        VisualStateManager.GoToState(page1, "Enabled", useTransitions: false);

    private void OnDisable(object sender, RoutedEventArgs e) =>
        VisualStateManager.GoToState(page1, "Disabled", useTransitions: false);
}
