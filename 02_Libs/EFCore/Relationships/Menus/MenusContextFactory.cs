using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;


public class BankContextFactory : IDesignTimeDbContextFactory<BankContext>
{
    public BankContext CreateDbContext(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Enter the connection string");
            Environment.Exit(-1);
        }
        string connectionString = args[0];

        DbContextOptionsBuilder<BankContext> builder = new();
        builder.UseSqlServer(connectionString);
        return new BankContext(builder.Options);
    }
}
