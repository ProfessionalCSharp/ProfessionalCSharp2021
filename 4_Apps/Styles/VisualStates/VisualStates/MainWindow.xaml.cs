using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace VisualStates
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

        private void OnEnable(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(page1, "Enabled", useTransitions: true);
        }

        private void OnDisable(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(page1, "Disabled", useTransitions: true);
        }
    }
}
