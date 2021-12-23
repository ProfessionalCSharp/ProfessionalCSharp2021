using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using WindowsAppChatClient.ViewModels;

namespace WinAppChatClient.Views;

public sealed partial class ChatUC : UserControl
{
    public ChatUC()
    {
        this.InitializeComponent();

        if (Application.Current is App app)
        {
            _scope = app.Services.CreateScope();
            ViewModel = _scope.ServiceProvider.GetRequiredService<ChatViewModel>();
        }
        else
        {
            throw new InvalidOperationException("Application.Current is not App");
        }
    }

    private readonly IServiceScope? _scope;
    public ChatViewModel ViewModel { get; private set; }
}
