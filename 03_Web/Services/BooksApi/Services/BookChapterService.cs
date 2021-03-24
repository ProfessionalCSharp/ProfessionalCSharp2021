using Books.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksApi.Services
{
    public class BookChapterService : IBookChapterService
    {
        private readonly ConcurrentDictionary<Guid, BookChapter> _chapters =
          new ConcurrentDictionary<Guid, BookChapter>();

        private BookChapter GetInitializedId(BookChapter chapter)
        {
            if (chapter.Id == Guid.Empty)
            {
                chapter = chapter with { Id = Guid.NewGuid() };
            }
            return chapter;
        }

        public Task AddAsync(BookChapter chapter)
        {
            chapter = GetInitializedId(chapter);
            _chapters[chapter.Id] = chapter;
            return Task.CompletedTask;
        }

        public Task AddRangeAsync(IEnumerable<BookChapter> chapters)
        {
            foreach (var chapter in chapters)
            {
                var chapteri = GetInitializedId(chapter);
                _chapters[chapteri.Id] = chapteri;
            }
            return Task.CompletedTask;
        }

        public Task<BookChapter?> FindAsync(Guid id)
        {
            _chapters.TryGetValue(id, out BookChapter? chapter);
            return Task.FromResult(chapter);
        }

        public Task<IEnumerable<BookChapter>> GetAllAsync() =>
            Task.FromResult<IEnumerable<BookChapter>>(_chapters.Values);

        public Task<BookChapter?> RemoveAsync(Guid id)
        {
            _chapters.TryRemove(id, out BookChapter? removed);
            return Task.FromResult(removed);
        }

        public async Task<BookChapter?> UpdateAsync(BookChapter chapter)
        {
            var existingChapter = await FindAsync(chapter.Id);
            if (existingChapter is null) return null;
            _chapters[chapter.Id] = chapter;
            return chapter;
        }
    }
}
