using System;
using System.Net.Http.Headers;

static class Utilities
{
    public static void ShowHeaders(string title, HttpHeaders headers)
    {
        Console.WriteLine(title);
        foreach (var header in headers)
        {
            string value = string.Join(" ", header.Value);
            Console.WriteLine($"Header: {header.Key} Value: {value}");
        }
        Console.WriteLine();
    }
}

