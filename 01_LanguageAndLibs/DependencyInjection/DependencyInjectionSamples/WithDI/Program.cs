using System;
using WithDI;

var controller = new HomeController(new GreetingService());
string result = controller.Hello("Matthias");
Console.WriteLine(result);
