public class Chapter
{
    public Chapter(string title, int chapterId = 0)
    {
        Title = title;
        ChapterId = chapterId;
    }
    public int ChapterId { get; set; }
    public string Title { get; set; }
}

