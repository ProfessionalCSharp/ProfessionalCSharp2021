using Books.Models;
using Books.Services;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRPCService.Services
{
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
            var chapter = request.Chapter;
            var bookChapter = new BookChapter(Guid.Parse(chapter.Id), chapter.Number, chapter.Title, chapter.PageCount);
            await _bookChapterService.AddAsync(bookChapter);
            AddBookChapterResponse response = new()
            {
                Chapter = new Chapter { Id = bookChapter.Id.ToString(), Title = bookChapter.Title, Number = bookChapter.Number, PageCount = bookChapter.PageCount }
            };
            return response;

        }

        //public override Task<GetBookChapterResponse> GetBookChapters(Empty request, ServerCallContext context)
        //{
        //    return Task.FromResult(new HelloReply
        //    {
        //        Message = "Hello " + request.Name
        //    });
        //}
    }
}
