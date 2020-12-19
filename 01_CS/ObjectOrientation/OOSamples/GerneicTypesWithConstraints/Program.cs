using System;

LinkedList<Person> list4 = new();
list4.AddLast(new Person("Stephanie", "Nagel", "Mrs"));
list4.AddLast(new Person("Matthias", "Nagel", "Mr"));
list4.AddLast(new Person("Katharina", "Nagel", "Mrs"));

// show the first
Console.WriteLine(list4.First);

public record Person(string FirstName, string LastName, string Title) : ITitle { }
