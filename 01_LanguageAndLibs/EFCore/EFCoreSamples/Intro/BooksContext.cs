using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Intro
{
    public class BooksContext : DbContext
    {
        private const string ConnectionString = @"server=(localdb)\MSSQLLocalDb;database=ProCSharpBooks;trusted_connection=true";

        public static ILoggerFactory BooksLoggerFactory { get; } = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public DbSet<Book> Books { get; set; } = default!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(BooksLoggerFactory)
                .UseSqlServer(ConnectionString);

        }
    }
}
