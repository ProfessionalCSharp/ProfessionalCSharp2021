using System;
using SampleApp;

HelloControl hello = new();
string greeting = hello.GreetService("Katharina");
Console.WriteLine(greeting);

CodeGenerationSample.HelloWorld.Hello();
