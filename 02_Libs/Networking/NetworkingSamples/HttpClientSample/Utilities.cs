using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


class Utilities
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

