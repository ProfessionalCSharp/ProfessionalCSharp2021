namespace Blazor.ComponentsSample.Shared;

public class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Publisher { get; set; }
    public DateTime ReleaseDate { get; set; }
}
