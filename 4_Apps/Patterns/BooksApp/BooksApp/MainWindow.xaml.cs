using BooksApp.ViewModels;
using BooksLib.Events;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BooksApp
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.SizeChanged += OnSizeChanged;
            ViewModel = Ioc.Default.GetRequiredService<MainWindowViewModel>();
            ViewModel.SetNavigationFrame(MainFrame);
        }

        private void OnSizeChanged(object sender, WindowSizeChangedEventArgs args)
        {
            double width = args.Size.Width;
            NavigationMessage navigation = new(new()
            {
                UseNavigation = width < 1024
            });
            WeakReferenceMessenger.Default.Send(navigation);
        }

        public MainWindowViewModel ViewModel { get; }

       
    }
}
