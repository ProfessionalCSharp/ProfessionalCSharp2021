LinkedList<Person> list4 = [];
list4.AddLast(new Person("Stephanie", "Nagel", "Mrs"));
list4.AddLast(new Person("Matthias", "Nagel", "Mr"));
list4.AddLast(new Person("Katharina", "Nagel", "Mrs"));

// show the first
Console.WriteLine(list4.First);

public record class Person(string FirstName, string LastName, string Title) : ITitle { }
