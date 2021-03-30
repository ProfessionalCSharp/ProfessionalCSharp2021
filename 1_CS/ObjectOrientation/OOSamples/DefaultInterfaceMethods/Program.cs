using System;

ILogger logger = new ConsoleLogger();
logger.Log("message");
logger.Log(new Exception("sample exception"));

IEnumerableEx<string> names = new MyCollection<string> { "James", "Jack", "Jochen", "Sebastian", "Lewis", "Juan" };

var jNames = names.Where(n => n.StartsWith("J"));
foreach (var name in jNames)
{
    Console.WriteLine(name);
}