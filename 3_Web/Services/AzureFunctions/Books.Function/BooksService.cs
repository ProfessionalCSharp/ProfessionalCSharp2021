using Books.Models;
using Books.Services;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

using System.Net;

namespace Books.Function;

public class BooksService
{
    private readonly IBookChapterService _bookChapterService;
    public BooksService(IBookChapterService bookChapterService)
    {
        if (bookChapterService is null) throw new ArgumentNullException(nameof(bookChapterService));
        _bookChapterService = bookChapterService;
    }

    [Function("AddChapter")]
    public async Task<HttpResponseData> AddChapterAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "chapters")] HttpRequestData req,
        FunctionContext executionContext)
    {
        var logger = executionContext.GetLogger("BooksService");
        logger.LogInformation("Function AddChapter invoked.");

        var chapter = await req.ReadFromJsonAsync<BookChapter>();
        if (chapter is null)
        {
            logger.LogError("invalid chapter received");
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }
        var response = req.CreateResponse(HttpStatusCode.OK);
        await _bookChapterService.AddAsync(chapter);
        await response.WriteAsJsonAsync(chapter);
        return response;
    }

    [Function("GetChapters")]
    public async Task<HttpResponseData> GetChaptersAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "chapters")] HttpRequestData req,
        FunctionContext executionContext)
    {
        var logger = executionContext.GetLogger("BooksService");
        logger.LogInformation("Function GetChapters invoked.");

        var response = req.CreateResponse(HttpStatusCode.OK);
        var chapters = _bookChapterService.GetAllAsync();
        await response.WriteAsJsonAsync(chapters);
        return response;
    }
}
