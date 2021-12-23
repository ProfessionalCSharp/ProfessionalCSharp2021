using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.UI.Xaml;

namespace WindowsAppAnalytics;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        AppCenter.Start("add your appcenter.ms app-id here!",
               typeof(Analytics), typeof(Crashes));
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        m_window = new MainWindow();
        m_window.Activate();
    }

    private Window? m_window;
}
