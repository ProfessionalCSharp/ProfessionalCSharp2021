namespace BooksApp.Services;

public class WinUIDialogService : IDialogService
{
    public async Task ShowMessageAsync(string message)
    {
        ContentDialog dlg = new()
        {
            Title = "Message",
            Content = message,
            PrimaryButtonText = "OK",   
        };
        await dlg.ShowAsync();
    }
}
