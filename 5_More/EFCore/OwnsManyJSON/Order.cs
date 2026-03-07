namespace OwnsMany;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string CustomerName { get; set; } = "";
    public OrderStatus Status { get; set; }
    public List<OrderLine> Lines { get; set; } = [];
}


