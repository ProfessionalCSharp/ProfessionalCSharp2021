namespace LoadingRelatedData;

public class Chapter(string title, int chapterId = 0)
{
    public int ChapterId { get; set; } = chapterId;
    public string Title { get; set; } = title;
    public int BookId { get; set; }
    public virtual Book? Book { get; set; }
}
