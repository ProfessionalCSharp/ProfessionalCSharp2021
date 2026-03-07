namespace AutoInclude;

public class Book(string title, string? publisher = default, int bookId = default)
{
    [StringLength(50)]
    public string Title { get; set; } = title;
    [StringLength(30)]
    public string? Publisher { get; set; } = publisher;
    public int BookId { get; set; } = bookId;

    // set accessor required for lazy loading
    public virtual ICollection<Chapter> Chapters { get; protected set; } = new HashSet<Chapter>();

    public int AuthorId { get; set; }
    [ForeignKey(nameof(AuthorId))]
    public virtual Person? Author { get; set; }
}
