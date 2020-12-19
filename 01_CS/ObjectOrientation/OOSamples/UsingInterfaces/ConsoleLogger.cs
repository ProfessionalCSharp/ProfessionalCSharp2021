using System;

public class ConsoleLogger : ILogger
{
     void ILogger.Log(string message) => Console.WriteLine(message);    
}
