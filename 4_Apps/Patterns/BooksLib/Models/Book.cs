using CommunityToolkit.Mvvm.ComponentModel;

namespace BooksLib.Models;

public class Book(string? title = null, string? publisher = null, int id = 0) : 
    ObservableObject
{
    public int BookId { get; set; } = id;
    private string _title = title ?? string.Empty;

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }
    private string _publisher = publisher ?? string.Empty;
    public string Publisher
    {
        get => _publisher;
        set => SetProperty(ref _publisher, value);
    }

    public override string ToString() => Title;
}
