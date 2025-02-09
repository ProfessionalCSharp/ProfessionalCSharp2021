﻿int x = 40;
GetAString firstStringMethod = new(x.ToString);
Console.WriteLine($"String is {firstStringMethod()}");
// With firstStringMethod initialized to x.ToString(),
// the above statement is equivalent to saying
// Console.WriteLine($"String is {x.ToString()}");

var balance = new Currency(34, 50);

// firstStringMethod references an instance method
firstStringMethod = balance.ToString;
Console.WriteLine($"String is {firstStringMethod()}");

// firstStringMethod references a static method
firstStringMethod = new GetAString(Currency.GetCurrencyUnit);
Console.WriteLine($"String is {firstStringMethod()}");

delegate string GetAString();