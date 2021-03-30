public abstract record Payment(string Name, decimal Amount, int PaymentId = 0);

public record CashPayment(string Name, decimal Amount, int PaymentId = 0) 
    : Payment(Name, Amount, PaymentId);

public record CreditcardPayment(string Name, decimal Amount, string CreditcardNumber, int PaymentId = 0) 
    : Payment(Name, Amount, PaymentId = 0);
