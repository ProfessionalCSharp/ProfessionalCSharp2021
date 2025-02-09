using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public abstract class Payment(string name, decimal amount, int paymentId = 0)
{
    public int PaymentId { get; init; } = paymentId;
    [StringLength(20)]
    public string Name { get; set; } = name;
    [Column(TypeName = "Money")]
    public decimal Amount { get; set; } = amount;
}

public class CashPayment : Payment
{
    public CashPayment(string name, decimal amount, int paymentId = 0)
        : base(name, amount, paymentId) {  }
}

public class CreditcardPayment(string name, decimal amount, int paymentId = 0) : Payment(name, amount, paymentId)
{
    public string? CreditcardNumber { get; set; }
}
