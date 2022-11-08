using WinRT.Interop;

namespace BooksApp.Services;

public class WinUIDialogService : IDialogService
{
    public async Task ShowMessageAsync(string message)
    {
        MessageDialog dlg = new(message);
        var hwnd = GetActiveWindow();
        if (hwnd == IntPtr.Zero)
        {
            throw new InvalidOperationException();
        }
        InitializeWithWindow.Initialize(dlg, hwnd);
        await dlg.ShowAsync();
    }

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();
}
