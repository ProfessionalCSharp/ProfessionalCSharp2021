namespace MultipleProvidersWithAspire.ApiService.Models;

public class Book(string title, string? publisher, int id = 0)
{
    public string Title { get; set; } = title;
    public string? Publisher { get; set; } = publisher;
    public int Id { get; set; } = id;
}
