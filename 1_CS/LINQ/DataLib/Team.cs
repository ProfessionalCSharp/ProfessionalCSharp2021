namespace DataLib;

public record Team
{
    public Team(string name, params int[] years)
    {
        Name = name;
        Years = years is not null ? [.. years] : [];
    }
    public string Name { get; }
    public IEnumerable<int> Years { get; }
}
