using System.Runtime.InteropServices;

using Windows.Foundation;
using Windows.UI.Popups;

using WinRT.Interop;

namespace WindowsAppChatClient.Services;

public class DialogService : IDialogService
{
    public async Task ShowMessageAsync(string message)
    {
        // await new MessageDialog(message).ShowAsync();
        await ShowDialogAsync(message);
    }

    public static IAsyncOperation<IUICommand> ShowDialogAsync(string content, string? title = null)
    {
        MessageDialog dlg = new(content, title ?? "");
        var hwnd = GetActiveWindow();
        if (hwnd == IntPtr.Zero)
            throw new InvalidOperationException();

        InitializeWithWindow.Initialize(dlg, hwnd);
        return dlg.ShowAsync();
    }

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();
}
