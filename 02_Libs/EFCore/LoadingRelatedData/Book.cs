using System.ComponentModel.DataAnnotations;

public class Book
{
    public Book(string title, string? publisher = default, int bookId = default)
    {
        Title = title;
        Publisher = publisher;
        BookId = bookId;
    }
    [StringLength(50)]
    public string Title { get; set; }
    [StringLength(30)]
    public string? Publisher { get; set; }
    public int BookId { get; set; }
}
