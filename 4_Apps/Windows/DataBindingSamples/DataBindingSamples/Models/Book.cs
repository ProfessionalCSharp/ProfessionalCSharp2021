namespace DataBindingSamples.Models;

public partial class Book(int id, string title, string publisher, params string[] authors) : ObservableObject
{
    public int BookId { get; } = id;
    private string _title = title;

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    private string _publisher = publisher;
    public string Publisher
    {
        get => _publisher;
        set => SetProperty(ref _publisher, value);
    }
    public IEnumerable<string> Authors { get; } = authors;

    public override string ToString() => Title;
}
