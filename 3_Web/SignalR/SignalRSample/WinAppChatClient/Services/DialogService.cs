using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Popups;
using WinRT;

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
        var handle = GetActiveWindow();
        if (handle == IntPtr.Zero)
            throw new InvalidOperationException();
        dlg.As<IInitializeWithWindow>().Initialize(handle);
        return dlg.ShowAsync();
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("3E68D4BD-7135-4D10-8018-9FB6D9F33FA1")]
    internal interface IInitializeWithWindow
    {
        void Initialize(IntPtr hwnd);
    }

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();
}
