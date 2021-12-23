public class Restaurant
{
    public Restaurant(string name, Guid id = default) => (_name, _id) = (name, id);

    private Guid _id = default;
    private string _name;
    public string Name => _name;

    public override string ToString() => $"{Name}, {_id}";
}
