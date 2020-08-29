using System;

// This loop iterates through rows
for (int i = 0; i < 100; i += 10)
{
    // This loop iterates through columns
    for (int j = i; j < i + 10; j++)
    {
        Console.Write($"  {j}");
    }
    Console.WriteLine();
}
