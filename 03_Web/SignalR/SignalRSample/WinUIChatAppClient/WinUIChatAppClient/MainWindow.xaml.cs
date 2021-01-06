using ChatClient.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUIChatAppClient
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {

        private IServiceScope? _serviceScope;
        public MainWindow()
        {
            _serviceScope = (Application.Current as App)!.Services.CreateScope();
            ViewModel = _serviceScope.ServiceProvider.GetRequiredService<MainWindowViewModel>();
            this.InitializeComponent();
        }

        public MainWindowViewModel ViewModel { get; }
    }
}
