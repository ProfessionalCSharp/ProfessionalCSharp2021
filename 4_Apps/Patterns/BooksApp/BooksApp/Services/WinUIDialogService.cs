namespace BooksApp.Services;

public class WinUIDialogService(Window window) : IDialogService
{
    public async Task ShowMessageAsync(string message)
    {
        ContentDialog dlg = new()
        {
            Title = "Message",
            Content = message,
            PrimaryButtonText = "OK",
            XamlRoot = window.Content.XamlRoot,
        };
        await dlg.ShowAsync();
    }
}
