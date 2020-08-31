using System;
using System.Collections.Generic;
using Wrox.ProCSharp.Generics;

var accounts = new List<Account>()
{
    new Account("Christian", 1500),
    new Account("Stephanie", 2200),
    new Account("Angela", 1800),
    new Account("Matthias", 2400),
    new Account("Katharina", 3800)
};

decimal amount = Algorithms.AccumulateSimple(accounts);
Console.WriteLine($"result of {nameof(Algorithms.AccumulateSimple)}: {amount}");

amount = Algorithms.Accumulate(accounts);
Console.WriteLine($"result of {nameof(Algorithms.Accumulate)}: {amount}");

amount = Algorithms.Accumulate<Account, decimal>(accounts, (item, sum) => sum += item.Balance);
Console.WriteLine($"result of generic {nameof(Algorithms.Accumulate)}: {amount}");

