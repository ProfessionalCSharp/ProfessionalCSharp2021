using System;

string s1 = new string("Hello, World!");
string s2 = "Hello, World!";
var s3 = "Hello, World!";
string s4 = new("Hello, World!");

Console.WriteLine(s1);
Console.WriteLine(s2);
Console.WriteLine(s3);
Console.WriteLine(s4);


void Method()
{
    Console.WriteLine("this is a method");
}

Method();

// create an instance of the Book type, set the property, and invoke the ToString method passing the object to WriteLine

Book b1 = new();
b1.Title = "Professional C#";
Console.WriteLine(b1);

#nullable disable

class Book
{
    public string Title { get; set; }
    public override string ToString() => Title;
}

#nullable restore
