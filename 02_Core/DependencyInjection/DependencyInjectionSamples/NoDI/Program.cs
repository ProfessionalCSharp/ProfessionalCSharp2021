using NoDI;
using System;

var controller = new HomeController();
string result = controller.Hello("Stephanie");
Console.WriteLine(result);

