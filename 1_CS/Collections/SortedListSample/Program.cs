using System.Collections.Generic;
using System;

SortedList<string, string> books = new();
books.Add("Front-end Development with ASP.NET Core", "978-1-119-18140-8");
books.Add("Beginning C# 7 Programming", "978-1-119-45866-1");

books["Enterprise Services"] = "978-0321246738";
books["Professional C# 7 and .NET Core 2.1"] = "978-1-119-44926-3";

foreach (KeyValuePair<string, string> book in books)
{
    Console.WriteLine($"{book.Key}, {book.Value}");
}

foreach (string isbn in books.Values)
{
    Console.WriteLine(isbn);
}

foreach (string title in books.Keys)
{
    Console.WriteLine(title);
}

{
    string title = "Professional C# 10";
    if (!books.TryGetValue(title, out string? isbn))
    {
        Console.WriteLine($"{title} not found");
    }
    else
    {
        Console.WriteLine($"{title} found: {isbn}");
    }
}

