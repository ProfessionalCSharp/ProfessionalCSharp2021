dynamic dyn = 100;
Console.WriteLine(dyn.GetType());
Console.WriteLine(dyn);

dyn = "This is a string";
Console.WriteLine(dyn.GetType());
Console.WriteLine(dyn);

dyn = new Person() { FirstName = "Bugs", LastName = "Bunny" };
Console.WriteLine(dyn.GetType());
Console.WriteLine($"{dyn.FirstName} {dyn.LastName}");

UseExpando();
Console.Read();

void UseExpando()
{
    dynamic expObj = new ExpandoObject();
    expObj.FirstName = "Daffy";
    expObj.LastName = "Duck";
    Console.WriteLine($"{expObj.FirstName} {expObj.LastName}");

    expObj.GetNextDay = new Func<DateTime, string>(day => day.AddDays(1).ToString("d"));

    Console.WriteLine($"next day: {expObj.GetNextDay(new DateTime(2021, 1, 3))}");

    expObj.Friends = new List<Person>();
    expObj.Friends.Add(new Person() { FirstName = "Bob", LastName = "Jones" });
    expObj.Friends.Add(new Person() { FirstName = "Robert", LastName = "Jones" });
    expObj.Friends.Add(new Person() { FirstName = "Bobby", LastName = "Jones" });

    foreach (dynamic friend in expObj.Friends)
    {
        Console.WriteLine($"{friend.FirstName} {friend.LastName}");
    }
}
