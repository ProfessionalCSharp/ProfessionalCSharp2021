using Books.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books.Services
{
    public interface IBookChapterService
    {
        Task AddAsync(BookChapter chapter);
        Task AddRangeAsync(IEnumerable<BookChapter> chapters);
        Task<IEnumerable<BookChapter>> GetAllAsync();
        Task<BookChapter?> FindAsync(Guid id);
        Task<BookChapter?> RemoveAsync(Guid id);
        Task<BookChapter?> UpdateAsync(BookChapter chapter);
        DatabaseFacade Database { get; }
    }
}
