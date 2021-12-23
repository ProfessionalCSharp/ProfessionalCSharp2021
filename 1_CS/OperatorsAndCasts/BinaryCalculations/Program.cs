long myLongNumber = 333333423;
object myObject = (object)myLongNumber;
int myIntNumber = (int)(long)myObject;

SimpleCalculations();
ShiftingBits();
SignedNumbers();

void SignedNumbers()
{
    Console.WriteLine(nameof(SignedNumbers));

    void DisplayNumber(string title, short x) =>
        Console.WriteLine($"{title,-12} bin: {x.ToBinaryString().AddSeparators()}, dec: {x,6}, hex: {x,4:X}");

    short maxNumber = short.MaxValue;
    DisplayNumber("max short", maxNumber);
    for (int i = 0; i < 3; i++)
    {
        maxNumber++;
        DisplayNumber($"added {i + 1}", maxNumber);
    }
    Console.WriteLine();

    short zero = 0;
    DisplayNumber("zero", zero);
    for (int i = 0; i < 3; i++)
    {
        zero--;
        DisplayNumber($"subtracted {i + 1}", zero);
    }
    Console.WriteLine();

    short minNumber = short.MinValue;
    DisplayNumber("min number", minNumber);
    for (int i = 0; i < 3; i++)
    {
        minNumber++;
        DisplayNumber($"added {i + 1}", minNumber);
    }
    Console.WriteLine();
}

void SimpleCalculations()
{
    Console.WriteLine(nameof(SimpleCalculations));
    uint binary1 = 0b1111_0000_1100_0011_1110_0001_0001_1000;
    uint binary2 = 0b0000_1111_1100_0011_0101_1010_1110_0111;
    uint binaryAnd = binary1 & binary2;
    DisplayBits("AND", binaryAnd, binary1, binary2);
    uint binaryOR = binary1 | binary2;
    DisplayBits("OR", binaryOR, binary1, binary2);
    uint binaryXOR = binary1 ^ binary2;
    DisplayBits("XOR", binaryXOR, binary1, binary2);
    uint reverse1 = ~binary1;
    DisplayBits("NOT", reverse1, binary1);
    Console.WriteLine();
}

void DisplayBits(string title, uint result, uint left, uint? right = null)
{
    Console.WriteLine(title);
    Console.WriteLine(left.ToBinaryString().AddSeparators());
    if (right.HasValue)
    {
        Console.WriteLine(right.Value.ToBinaryString().AddSeparators());
    }
    Console.WriteLine(result.ToBinaryString().AddSeparators());
    Console.WriteLine();
}

void ShiftingBits()
{
    Console.WriteLine(nameof(ShiftingBits));
    ushort s1 = 0b01;
    Console.WriteLine($"{"Binary",16} {"Decimal",8} {"Hex",6}");
    for (int i = 0; i < 16; i++)
    {
        Console.WriteLine($"{s1.ToBinaryString(),16} {s1,8} {s1,6:X}");
        s1 = (ushort)(s1 << 1);
    }
    Console.WriteLine();
}
