LinkedList<int> list1 = new();
list1.AddLast(1);
list1.AddLast(3);
list1.AddLast(2);

foreach (var item in list1)
{
    Console.WriteLine(item);
}
Console.WriteLine();

LinkedList<string> list2 = new();
list2.AddLast("two");
list2.AddLast("four");
list2.AddLast("six");

// show the last
Console.WriteLine(list2.Last);

LinkedList<(int, int)> list3 = new();
list3.AddLast((1, 2));
list3.AddLast((3, 4));
foreach (var item in list3)
{
    Console.WriteLine(item);
}
Console.WriteLine();

LinkedList<Person> list4 = new();
list4.AddLast(new Person("Stephanie", "Nagel"));
list4.AddLast(new Person("Matthias", "Nagel"));
list4.AddLast(new Person("Katharina", "Nagel"));

// show the first
Console.WriteLine(list4.First);

public record Person(string FirstName, string LastName);
