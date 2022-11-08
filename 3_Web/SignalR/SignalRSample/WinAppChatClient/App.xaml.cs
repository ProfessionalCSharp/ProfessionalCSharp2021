using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;

using WindowsAppChatClient.Services;
using WindowsAppChatClient.ViewModels;

namespace WinAppChatClient;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddScoped<IDialogService, DialogService>();
                services.AddScoped<UrlService>();
                services.AddScoped<ChatViewModel>();
                services.AddScoped<GroupChatViewModel>();
            }).Build();
    }

    private readonly IHost _host;

    internal IServiceProvider Services => _host.Services;

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        _window = new MainWindow();
        _window.Activate();
    }

    private Window? _window;
}
