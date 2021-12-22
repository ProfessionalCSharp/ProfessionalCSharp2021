using Microsoft.UI.Xaml.Controls;

namespace BooksApp.Services;

public class WinUIInitializeNavigationService
{
    public void Initialize(Frame frame, Dictionary<string, Type> pages)
    {
        _frame = frame ?? throw new ArgumentNullException(nameof(frame));
        _pages = pages ?? throw new ArgumentNullException(nameof(pages));
    }

    private Frame? _frame;
    public Frame Frame => _frame ?? throw new InvalidOperationException($"{nameof(WinUIInitializeNavigationService)} not initalized");

    private Dictionary<string, Type>? _pages;
    public Dictionary<string, Type> Pages => _pages ?? throw new InvalidOperationException($"{nameof(WinUIInitializeNavigationService)} not initalized");
}
