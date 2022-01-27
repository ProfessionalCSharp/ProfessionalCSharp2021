using Books.Models;

namespace Books.Services;

public interface IBookChapterService
{
    Task<BookChapter> AddAsync(BookChapter chapter);
    Task AddRangeAsync(IEnumerable<BookChapter> chapters);
    Task<IEnumerable<BookChapter>> GetAllAsync();
    Task<BookChapter?> FindAsync(Guid id);
    Task<BookChapter?> RemoveAsync(Guid id);
    Task<BookChapter?> UpdateAsync(BookChapter chapter);
}
