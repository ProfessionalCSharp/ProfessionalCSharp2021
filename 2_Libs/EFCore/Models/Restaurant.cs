public class Restaurant
{
    public Restaurant(string name, int id = default) => (_name, _id) = (name, id);

    private int _id = default;
    private string _name;
    public string Name => _name;

    public override string ToString() => $"{Name}, {_id}";
}
