public class BankRunner
{
    private readonly BankContext _bankContext;

    public BankRunner(BankContext bankContext) => _bankContext = bankContext;

    public async Task CreateTheDatabaseAsync()
    {
        await _bankContext.Database.MigrateAsync();
    }

    public async Task AddSampleDataAsync()
    {
        _bankContext.Payments.Add(new CashPayment("Donald", 0.5M));
        _bankContext.Payments.Add(new CashPayment("Scrooge", 20000M));
        _bankContext.Payments.Add(new CreditcardPayment("Gus Goose", 300M) 
        {
            CreditcardNumber = "987654321"
        });
        await _bankContext.SaveChangesAsync();
    }

    public async Task QuerySampleAsync()
    {
        var creditcardPayments = await _bankContext.Payments
            .OfType<CreditcardPayment>()
            .ToListAsync();
        foreach (var payment in creditcardPayments)
        {
            Console.WriteLine($"{payment.Name}, {payment.Amount}");
        }
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
