using System.ComponentModel.DataAnnotations;

#if USERECORDS

public record Book(
    [property: StringLength(50)] string Title,
    [property: StringLength(30)] string? Publisher = default,
    int BookId = 0);

#else

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

#endif