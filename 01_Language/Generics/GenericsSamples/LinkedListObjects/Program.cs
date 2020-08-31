using System;
using Wrox.ProCSharp.Generics;

var list1 = new LinkedList();
list1.AddLast(2);
list1.AddLast(4);
// list1.AddLast("6");

foreach (int i in list1)
{
    Console.WriteLine(i);
}

