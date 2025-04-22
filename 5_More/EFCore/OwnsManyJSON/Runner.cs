namespace OwnsMany;

public class Runner(OrdersContext ordersContext)
{
    public async Task CreateTheDatabaseAsync()
    {
        bool created = await ordersContext.Database.EnsureCreatedAsync();
        string creationInfo = created ? "created" : "exists";
        Console.WriteLine($"database {creationInfo}");
    }

    public async Task CreateOrdersAsync()
    {
        await ordersContext.CreateDataAsync();
    }

    public async Task QueryOrdersAsync()
    {
        var orders = await ordersContext.Orders.ToListAsync();
        Console.WriteLine();
    }

    public async Task DeleteDatabaseAsync()
    {
        Console.Write("Delete the database? (y|n) ");
        string? input = Console.ReadLine();
        if (input?.ToLower() == "y")
        {
            bool deleted = await ordersContext.Database.EnsureDeletedAsync();
            string deletionInfo = deleted ? "deleted" : "not deleted";
            Console.WriteLine($"database {deletionInfo}");
        }
    }
}
