using System.ComponentModel.DataAnnotations;
namespace ConfictHandling;

public class Book(string title, string? publisher = default, int bookId = default)
{
    [StringLength(50)]
    public string Title { get; set; } = title;
    [StringLength(30)]
    public string? Publisher { get; set; } = publisher;
    public int BookId { get; set; } = bookId;
}