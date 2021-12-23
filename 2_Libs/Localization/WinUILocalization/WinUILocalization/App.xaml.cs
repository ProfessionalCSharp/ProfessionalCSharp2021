using Microsoft.UI.Xaml;

using System.Runtime.InteropServices;

using Microsoft.Windows.ApplicationModel.Resources;

using WinRT;

namespace WinUILocalization;

public partial class App : Application
{
    public App() => InitializeComponent();

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        ResourceLoader resourceLoader = new();
        ResourceManager resourceManager = new();
        m_window = new MainWindow(resourceLoader, resourceManager);
        var windowNative = m_window.As<IWindowNative>();
        m_window.Title = "WinUI Resources";
        m_window.Activate();
    }

    private Window? m_window;
}

[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("EECDBF0E-BAE9-4CB6-A68E-9598E1CB57BB")]
internal interface IWindowNative
{
    IntPtr WindowHandle { get; }
}
