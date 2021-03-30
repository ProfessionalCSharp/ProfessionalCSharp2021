using Books.Models;
using Books.Services;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GRPCService.Services
{
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

    public class BooksService : GRPCBooks.GRPCBooksBase
    {
        private readonly IBookChapterService _bookChapterService;
        private readonly ILogger _logger;
        public BooksService(ILogger<BooksService> logger, IBookChapterService bookChapterService)
        {
            _logger = logger;
            _bookChapterService = bookChapterService;
        }

        public override async Task<AddBookChapterResponse> AddBookChapter(AddBookChapterRequest request, ServerCallContext context)
        {
            var bookChapter = request.Chapter.ToBookChapter();
            await _bookChapterService.AddAsync(bookChapter);
            AddBookChapterResponse response = new()
            {
                Chapter = bookChapter.ToGRPCChapter()
            };
            return response;
        }

        public override async Task<GetBookChapterResponse> GetBookChapters(Empty request, ServerCallContext context)
        {
            var bookChapters = await _bookChapterService.GetAllAsync();
            GetBookChapterResponse response = new();
            response.Chapters.AddRange(bookChapters.Select(bc => bc.ToGRPCChapter()).ToArray());
            return response;
        }
    }
}
