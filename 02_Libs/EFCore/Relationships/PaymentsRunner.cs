public abstract class Payment
{
    public Payment(string name, decimal amount, int paymentId = 0)
    {
        Name = name;
        Amount = amount;
        PaymentId = paymentId;
    }
    public int PaymentId { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
}

public class CashPayment : Payment
{
    public CashPayment(string name, decimal amount, int paymentId = 0)
        : base(name, amount, paymentId)
    {

    }
}

public class CreditcardPayment : Payment
{
    public CreditcardPayment(string name, decimal amount, int paymentId = 0)
    : base(name, amount, paymentId)
    {

    }
    public string? CreditcardNumber { get; set; }
}
