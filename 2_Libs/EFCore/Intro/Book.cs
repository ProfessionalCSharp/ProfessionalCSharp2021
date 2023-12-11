using System.ComponentModel.DataAnnotations;

#if USERECORDS

public record Book(
    [property: StringLength(50)] string Title,
    [property: StringLength(30)] string? Publisher = default,
    int BookId = 0);

#else

public class Book(string title, string? publisher = default, int bookId = default)
{
    [StringLength(50)]
    public string Title { get; set; } = title;
    [StringLength(30)]
    public string? Publisher { get; set; } = publisher;
    public int BookId { get; set; } = bookId;
}

#endif