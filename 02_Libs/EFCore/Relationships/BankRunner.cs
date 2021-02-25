using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

public class BankRunner
{
    private readonly BankContext _bankContext;

    public BankRunner(BankContext bankContext) => _bankContext = bankContext;

    public async Task CreateTheDatabaseAsync()
    {
        await _bankContext.Database.MigrateAsync();
    }

    public async Task DeleteDatabaseAsync()
    {
        Console.Write("Delete the database? (y|n) ");
        string? input = Console.ReadLine();
        if (input?.ToLower() == "y")
        {
            bool deleted = await _bankContext.Database.EnsureDeletedAsync();
            string deletionInfo = deleted ? "deleted" : "not deleted";
            Console.WriteLine($"database {deletionInfo}");
        }
    }


}
