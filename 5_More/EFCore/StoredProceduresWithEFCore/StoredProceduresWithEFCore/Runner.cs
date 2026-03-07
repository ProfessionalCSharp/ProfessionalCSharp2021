using StoredProceduresWithEFCore.Data;

namespace StoredProceduresWithEFCore;
internal class Runner(BooksContext booksContext)
{
    public async Task RunAsync()
    {
        // Example of how to use the context
        var books = booksContext.Books.ToList();
        foreach (var book in books)
        {
            Console.WriteLine($"Book: {book.Title}, Publisher: {book.Publisher}");
        }

        // Example of how to call the stored procedure
        var year = 2021;
        var booksByYear = await booksContext.GetBooksByYearAsync(year);
    }
}
