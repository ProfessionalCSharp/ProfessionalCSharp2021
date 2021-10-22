using Books.Models;

namespace BooksApi.Services;

public interface IBookChapterService
{
    Task AddAsync(BookChapter chapter);
    Task AddRangeAsync(IEnumerable<BookChapter> chapters);
    Task<IEnumerable<BookChapter>> GetAllAsync();
    Task<BookChapter?> FindAsync(Guid id);
    Task<BookChapter?> RemoveAsync(Guid id);
    Task<BookChapter?> UpdateAsync(BookChapter chapter);
}
