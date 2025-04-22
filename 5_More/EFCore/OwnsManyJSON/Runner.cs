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

    public async Task QueryJsonAsync()
    {
        var ordersWithMonitors = await ordersContext.Orders
            .AsNoTracking()
            .Where(o => o.Lines.Any(l => l.ProductName == "Monitor"))
            .Select(o => new
            {
                o.Id,
                o.CustomerName,
                o.OrderDate,
                MonitorLines = o.Lines.Where(l => l.ProductName == "Monitor")
            })
            .ToListAsync();

        foreach (var order in ordersWithMonitors)
        {
            Console.WriteLine($"\nOrder {order.Id} - {order.CustomerName} on {order.OrderDate:d}");
            foreach (var line in order.MonitorLines)
            {
                Console.WriteLine($"  Monitor - Quantity: {line.Quantity}, Price: ${line.UnitPrice}");
            }
        }
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
