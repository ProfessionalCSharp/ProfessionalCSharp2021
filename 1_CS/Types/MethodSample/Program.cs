using System;

// Try calling some static functions.
Console.WriteLine($"Pi is {Math.GetPi()}");
int x = Math.GetSquareOf(5);
Console.WriteLine($"Square of 5 is {x}");

// Instantiate a Math object
var math = new Math();   // instantiate a reference type

// Call instance members
math.Value = 30;
Console.WriteLine($"Value field of math variable contains {math.Value}");
Console.WriteLine($"Square of 30 is {math.GetSquare()}");

// invoking methnds with local functions

LocalFunctionsSample.IntroLocalFunctions();
LocalFunctionsSample.LocalFunctionWithClosure();

// using generic methods

int x1 = 3;
int y1 = 4;
GenericMethods.Swap(ref x1, ref y1);
Console.WriteLine($"new values - x1: {x1}, y1: {y1}");

string s1 = "one";
string s2 = "two";
GenericMethods.Swap(ref s1, ref s2);
Console.WriteLine($"new values - s1: {s1}, s2: {s2}");

Console.ReadLine();
