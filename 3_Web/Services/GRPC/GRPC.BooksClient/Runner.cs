using Books.Models;

using Google.Protobuf.WellKnownTypes;

using GRPCService;

using Microsoft.Extensions.Logging;

static class ChapterExtensions
{
    public static BookChapter ToBookChapter(this Chapter chapter) =>
        new BookChapter(
            Guid.Parse(chapter.Id),
            chapter.Number,
            chapter.Title,
            chapter.PageCount);

    public static Chapter ToGRPCChapter(this BookChapter chapter) =>
        new Chapter
        {
            Id = chapter.Id.ToString(),
            Number = chapter.Number,
            Title = chapter.Title,
            PageCount = chapter.PageCount
        };
}

public class Runner
{
    private readonly GRPCBooks.GRPCBooksClient _booksClient;
    private readonly ILogger _logger;
    public Runner(GRPCBooks.GRPCBooksClient booksClient, ILogger<Runner> logger)
    {
        _booksClient = booksClient;
        _logger = logger;
    }

    public async Task RunAsync()
    {
        CancellationTokenSource cts = new(10000); // cancel after 10 seconds

        try
        {
            BookChapter bookChapter = new(Guid.NewGuid(), 43, "A new GPRC chapter", 20);
            AddBookChapterRequest request = new() 
            { 
                Chapter = bookChapter.ToGRPCChapter() 
            };
            var addBookResponse = await _booksClient.AddBookChapterAsync(request);
            Console.WriteLine($"added a new book");

            var getBookResponse = await _booksClient.GetBookChaptersAsync(new Empty());
            var bookChapters = getBookResponse.Chapters.Select(c => c.ToBookChapter()).ToArray();
            foreach (var chapter in bookChapters)
            {
                Console.WriteLine($"{chapter.Number}: {chapter.Title}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
