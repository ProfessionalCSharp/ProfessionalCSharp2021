namespace TransactionsSamples;

public class Restaurant(string name, Guid id = default)
{
#pragma warning disable IDE0032 // Use auto property - fields used in EF Core mapping
    private readonly Guid _id = id;

    private readonly string _name = name;
    public string Name => 
        _name;
#pragma warning restore IDE0032 // Use auto property

    public override string ToString() => $"{Name}, {_id}";
}
