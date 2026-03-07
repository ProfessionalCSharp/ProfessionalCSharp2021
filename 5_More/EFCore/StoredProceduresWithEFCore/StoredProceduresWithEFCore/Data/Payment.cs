namespace StoredProceduresWithEFCore.Data;

public partial class Payment
{
    public int PaymentId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Amount { get; set; }

    public string Type { get; set; } = null!;

    public string? CreditcardNumber { get; set; }
}
