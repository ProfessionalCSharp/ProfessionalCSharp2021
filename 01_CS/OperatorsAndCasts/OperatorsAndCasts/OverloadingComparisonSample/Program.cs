using System;

Vector vect1 = new (3.0, 3.0, -10.0);
Vector vect2 = new(3.0, 3.0, -10.0);
Vector vect3 = new(2.0, 3.0, 6.0);

Console.WriteLine($"vect1 == vect2 returns {(vect1 == vect2)}");
Console.WriteLine($"vect1 == vect3 returns {(vect1 == vect3)}");
Console.WriteLine($"vect2 == vect3 returns {(vect2 == vect3)}");

Console.WriteLine();

Console.WriteLine($"vect1 != vect2 returns {(vect1 != vect2)}");
Console.WriteLine($"vect1 != vect3 returns {(vect1 != vect3)}");
Console.WriteLine($"vect2 != vect3 returns {(vect2 != vect3)}");

Vector vect4 = new(5.0, 2.0, 0);
Vector vect5 = new(2.0, 5.0, 0);
Console.WriteLine(vect4.GetHashCode());
Console.WriteLine(vect5.GetHashCode());
