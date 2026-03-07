namespace StoredProceduresWithEFCore.Data;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string? Publisher { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public virtual ICollection<Person> AuthorsPeople { get; set; } = new List<Person>();
}
