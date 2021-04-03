using System;

decimal d1 = 7.94m;
decimal d2 = d1 * d1;
Console.WriteLine(d2);

DoubleOp[] operations =
{
    MathOperations.MultiplyByTwo,
    MathOperations.Square
};

for (int i = 0; i < operations.Length; i++)
{
    Console.WriteLine($"Using operations[{i}]:");
    ProcessAndDisplayNumber(operations[i], 2.0);
    ProcessAndDisplayNumber(operations[i], 7.94);
    ProcessAndDisplayNumber(operations[i], 1.414);
    Console.WriteLine();
}

void ProcessAndDisplayNumber(DoubleOp action, double value)
{
    Console.WriteLine($"Value is {value}, result of operation is {action(value)}");
}

delegate double DoubleOp(double x);