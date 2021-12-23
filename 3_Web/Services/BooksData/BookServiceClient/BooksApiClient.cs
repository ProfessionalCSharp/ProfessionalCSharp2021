using Books.Models;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System.Net.Http.Json;

namespace BookServiceClient;

public record BooksApiClientOptions
{
    public string? BooksApiUri { get; init; }
}

public class BooksApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _booksApiUri;
    private readonly ILogger _logger;
    private Guid? _firstChapterId;

    public BooksApiClient(HttpClient httpClient, IOptions<BooksApiClientOptions> options, ILogger<BooksApiClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _booksApiUri = options.Value.BooksApiUri ?? "api/books";
    }

    public async Task ReadChaptersAsync()
    {
        Console.WriteLine(nameof(ReadChapterAsync));
        var chapters = await _httpClient.GetFromJsonAsync<IEnumerable<BookChapter>>(_booksApiUri);
        if (chapters is null) return;
        foreach (var chapter in chapters)
        {
            Console.WriteLine($"{chapter.Number} {chapter.Title}");
        }
        _firstChapterId = chapters.FirstOrDefault()?.Id;
        Console.WriteLine();
    }

    public async Task ReadChapterAsync()
    {
        Console.WriteLine(nameof(ReadChapterAsync));
        if (_firstChapterId is not null)
        {
            string uri = $"{_booksApiUri}/{_firstChapterId}";
            var chapter = await _httpClient.GetFromJsonAsync<BookChapter>(uri);
            if (chapter is not null)
            {
                Console.WriteLine($"{chapter.Number} {chapter.Title}");
            }
        }
        Console.WriteLine();
    }

    public async Task ReadNotExistingChapterAsync()
    {
        Console.WriteLine(nameof(ReadNotExistingChapterAsync));
        string requestIdentifier = Guid.NewGuid().ToString();
        try
        {
            string uri = $"{_booksApiUri}/{requestIdentifier}";
            var chapter = await _httpClient.GetFromJsonAsync<BookChapter>(uri);
        }
        catch (HttpRequestException ex) when (ex.Message.Contains("404"))
        {
            _logger.LogError("book chapter with identifier {0} not found", requestIdentifier);
        }
        Console.WriteLine();
    }

    public async Task AddChapterAsync()
    {
        Console.WriteLine(nameof(AddChapterAsync));
        var chapter = new BookChapter(Guid.NewGuid(), 25, "Services", 40);
        var response = await _httpClient.PostAsJsonAsync(_booksApiUri, chapter);
        Console.WriteLine($"status code: {response.StatusCode}");
        Console.WriteLine($"created at location: {response.Headers.Location?.AbsolutePath}");
        Console.WriteLine();
    }

    public async Task UpdateChapterAsync()
    {
        Console.WriteLine(nameof(UpdateChapterAsync));

        var chapters = await _httpClient.GetFromJsonAsync<IEnumerable<BookChapter>>(_booksApiUri);
        if (chapters is null) return;
        var chapter = chapters.SingleOrDefault(
          c => c.Title == ".NET Application Architectures");
        if (chapter is not null)
        {
            string uri = $"{_booksApiUri}/{chapter.Id}";
            chapter = chapter with { Title = ".NET Applications and Tools" };
            var response = await _httpClient.PutAsJsonAsync(uri, chapter);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Status code: {response.StatusCode}");
                Console.WriteLine($"Updated chapter {chapter.Title}");
            }
        }
        Console.WriteLine();
    }

    public async Task RemoveChapterAsync()
    {
        Console.WriteLine(nameof(RemoveChapterAsync));
        var chapters = await _httpClient.GetFromJsonAsync<IEnumerable<BookChapter>>(_booksApiUri);
        if (chapters is null) return;

        var chapter = chapters.SingleOrDefault(c => c.Title == "ADO.NET and Transactions");
        if (chapter is not null)
        {
            string uri = $"{_booksApiUri}/{chapter.Id}";
            var response = await _httpClient.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"removed chapter {chapter.Title}");
            }
        }
        Console.WriteLine();
    }
}
