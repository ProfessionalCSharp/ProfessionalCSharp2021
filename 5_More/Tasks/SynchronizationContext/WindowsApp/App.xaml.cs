using Microsoft.UI.Xaml;
using Windows.ApplicationModel.Core;

namespace WindowsApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
        {

        };
        CoreApplication.UnhandledErrorDetected += (sender, e) =>
        {

        };
        UnhandledException += (sender, e) =>
        {

        };
    }

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        _window = new MainWindow();
        _window.Activate();
    }

    private Window? _window;
}
