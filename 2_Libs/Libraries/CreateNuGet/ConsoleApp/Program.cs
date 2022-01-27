using SampleLib;

Console.WriteLine(Demo.Show());
Book b = new() { Title = "Professional C#", Publisher = "Wrox Press"};
string json = Demo.GetJson(b); 
Console.WriteLine(json);
