using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

class Runner
{
    private readonly BankContext _bankContext;
    public Runner(BankContext bankContext)
    {
        _bankContext = bankContext;
    }

    public void QuerySample()
    {
        var creditcardPayments = _bankContext.Payments.OfType<CreditcardPayment>();
        foreach (var payment in creditcardPayments)
        {
            Console.WriteLine($"{payment.Name}, {payment.Amount}");
        }
    }

    public async Task AddSampleDataAsync()
    {
        _bankContext.Payments.Add(new CashPayment("Donald", 0.5M));
        _bankContext.Payments.Add(new CashPayment("Scrooge", 20000M));
        _bankContext.Payments.Add(new CreditcardPayment("Gus Goose", 300M, "987654321"));
        await _bankContext.SaveChangesAsync();
    }

    public async Task CreateDatabaseAsync()
    {
        bool created = await _bankContext.Database.EnsureCreatedAsync();
        string creationInfo = created ? "created" : "exists";
    }

    public async Task DeleteDatabaseAsync()
    {
        Console.Write("Delete the database? ");
        string? input = Console.ReadLine();
        if (input?.ToLower() == "y")
        {
            bool deleted = await _bankContext.Database.EnsureDeletedAsync();
            string deletionInfo = deleted ? "deleted" : "not deleted";
            Console.WriteLine($"database {deletionInfo}");
        }
    }
}

