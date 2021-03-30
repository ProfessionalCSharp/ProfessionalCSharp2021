using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    // set accessor required for lazy loading
    public virtual ICollection<Chapter> Chapters { get; protected set; } = new HashSet<Chapter>();

    public int AuthorId { get; set; }
    [ForeignKey(nameof(AuthorId))]
    public virtual Person? Author { get; set; }
}
