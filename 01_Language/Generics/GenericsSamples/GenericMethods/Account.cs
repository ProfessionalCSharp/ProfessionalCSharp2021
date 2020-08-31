namespace Wrox.ProCSharp.Generics
{
    public interface IAccount
    {
        decimal Balance { get; }
        string Name { get; }
    }

    public record Account(string Name, decimal Balance) : IAccount
    {
    }
}
