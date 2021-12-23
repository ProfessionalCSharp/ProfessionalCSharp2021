using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class BooksContextFactory : IDesignTimeDbContextFactory<BooksContext>
{
    public BooksContext CreateDbContext(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Enter the connection string");
            Environment.Exit(-1);
        }
        string connectionString = args[0];

        DbContextOptionsBuilder<BooksContext> builder = new();
        builder.UseSqlServer(connectionString);
        return new BooksContext(builder.Options);
    }
}
