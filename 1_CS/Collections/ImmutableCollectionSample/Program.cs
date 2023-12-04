using System.Collections.Immutable;

SimpleArrayDemo();
ImmutableList<Account> accounts = CreateImmutableList();
UsingABuilder(accounts);
LinqDemo();

Console.ReadLine();

void LinqDemo()
{
    ImmutableArray<string> arr = ["one", "two", "three", "four", "five"];
    var result = arr.Where(s => s.StartsWith('t'));
}

void UsingABuilder(ImmutableList<Account> immutableAccounts)
{
    var builder = immutableAccounts.ToBuilder();
    for (int i = builder.Count - 1; i >= 0; i--)
    {
        Account a = builder[i];
        if (a.Amount > 0)
        {
            builder.Remove(a);
        }
    }

    ImmutableList<Account> overdrawnAccounts = builder.ToImmutable();

    overdrawnAccounts.ForEach(a => Console.WriteLine($"overdrawn: {a.Name} {a.Amount}"));
}

ImmutableList<Account> CreateImmutableList()
{
    List<Account> accounts =
    [
        new("Scrooge McDuck", 667377678765m),
        new("Donald Duck", -200m),
        new("Ludwig von Drake", 20000m)
    ];

    ImmutableList<Account> immutableAccounts = [.. accounts];

    foreach (var account in immutableAccounts)
    {
        Console.WriteLine($"{account.Name} {account.Amount}");
    }

    immutableAccounts.ForEach(a => Console.WriteLine($"{a.Name} {a.Amount}"));

    return immutableAccounts;
}

void SimpleArrayDemo()
{
    ImmutableArray<string> a1 = [];
    ImmutableArray<string> a2 = a1.Add("Williams");
    ImmutableArray<string> a3 =
        a2.Add("Ferrari").Add("Mercedes").Add("Red Bull Racing");
}

public record Account(string Name, decimal Amount);