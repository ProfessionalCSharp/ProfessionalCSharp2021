using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

public class MenusContextFactory : IDesignTimeDbContextFactory<MenusContext>
{
    public MenusContext CreateDbContext(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Enter the connection string");
            Environment.Exit(-1);
        }
        string connectionString = args[0];

        DbContextOptionsBuilder<MenusContext> builder = new();
        builder.UseSqlServer(connectionString);
        return new MenusContext(builder.Options);
    }
}
