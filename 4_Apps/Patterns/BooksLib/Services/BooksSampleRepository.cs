using BooksLib.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksLib.Services
{
    public class BooksSampleRepository : IBooksRepository
    {
        private List<Book> _books;
        public BooksSampleRepository() =>
            _books = GetSampleBooks();

        private List<Book> GetSampleBooks() =>
            new()
            {
                new("Professional C# and .NET - 2021 Edition", "Wrox Press", 1),
                new("Professional C# 7 and .NET Core 2", "Wrox Press", 2),
                new("Professional C# 6 and .NET Core 1.0", "Wrox Press", 3),
                new("Professional C# 5.0 and .NET 4.5.1", "Wrox Press", 4),
                new("Enterprise Services with the .NET Framework", "AWL", 5)
            };

        public Task<bool> DeleteAsync(int id)
        {
            Book? bookToDelete = _books.Find(b => b.BookId == id);
            if (bookToDelete is not null)
            {
                return Task.FromResult(_books.Remove(bookToDelete));
            }
            return Task.FromResult(false);
        }

        public Task<Book?> GetItemAsync(int id) =>
            Task.FromResult(_books.Find(b => b.BookId == id));

        public Task<IEnumerable<Book>> GetItemsAsync() => 
            Task.FromResult<IEnumerable<Book>>(_books);

        public Task<Book> UpdateAsync(Book item)
        {
            Book bookToUpdate = _books.Single(b => b.BookId == item.BookId);
            int ix = _books.IndexOf(bookToUpdate);
            _books[ix] = item;
            return Task.FromResult(_books[ix]);
        }

        public Task<Book> AddAsync(Book item)
        {
            item.BookId = _books.Select(b => b.BookId).Max() + 1;
            _books.Add(item);
            return Task.FromResult(item);
        }
    }
}
