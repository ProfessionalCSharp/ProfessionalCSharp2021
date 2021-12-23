Person p1 = new("Ayrton", "Senna", new DateTime(1960, 3, 21));
Person p2 = new("Ronnie", "Peterson", new DateTime(1944, 2, 14));
Person p3 = new("Jochen", "Rindt", new DateTime(1942, 4, 18));
Person p4 = new("Francois", "Cevert", new DateTime(1944, 2, 25));
PersonCollection coll = new(p1, p2, p3, p4);

Console.WriteLine(coll[2]);

foreach (var r in coll[new DateTime(1960, 3, 21)])
{
    Console.WriteLine(r);
}
Console.ReadLine();