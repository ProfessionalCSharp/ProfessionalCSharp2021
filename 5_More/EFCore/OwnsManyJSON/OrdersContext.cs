namespace OwnsMany;

public class OrdersContext(DbContextOptions<OrdersContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .OwnsMany(o => o.Lines)
            .ToJson();
    }

    public async Task CreateDataAsync()
    {
        for (int i = 1; i <= 20; i++)
        {
            Order order = new()
            {
                OrderDate = DateTime.Now.AddDays(-Random.Shared.Next(1, 60)),
                CustomerName = $"Customer {i}",
                Status = (OrderStatus)(i % 3)
            };

            int lineCount = Random.Shared.Next(2, 5);
            for (int j = 1; j <= lineCount; j++)
            {
                order.Lines.Add(new OrderLine
                {
                    ProductId = Random.Shared.Next(1, 100),
                    ProductName = GetRandomProduct(),
                    Quantity = Random.Shared.Next(1, 10),
                    UnitPrice = decimal.Round((decimal)(Random.Shared.NextDouble() * 100), 2)
                });
            }
            Orders.Add(order);
        }

        await SaveChangesAsync();
    }

    private static string GetRandomProduct() => Random.Shared.Next(1, 10) switch
    {
        1 => "Coffee Maker",
        2 => "Laptop",
        3 => "Smartphone",
        4 => "Headphones",
        5 => "Monitor",
        6 => "Keyboard",
        7 => "Mouse",
        8 => "Printer",
        9 => "Scanner",
        _ => "Desk"
    };

    public DbSet<Order> Orders => Set<Order>();
}
