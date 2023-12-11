public class Restaurant
{
    public Restaurant(string name, Guid id = default) => (_name, _id) = (name, id);

    private readonly Guid _id;

#pragma warning disable IDE0044 // Add readonly modifier - _name is used with EF Core mapping
#pragma warning disable IDE0032 // Use auto property - _name is used with EF Core mapping
    private string _name;

    public string Name => _name;
#pragma warning restore IDE0032 // Use auto property
#pragma warning restore IDE0044 // Add readonly modifier

    public override string ToString() => $"{Name}, {_id}";
}
