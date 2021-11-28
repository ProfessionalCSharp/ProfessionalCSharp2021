BitArray bits1 = new(9);
bits1.SetAll(true);
bits1.Set(1, false);
bits1[5] = false;
bits1[7] = false;
Console.Write("initialized: ");
Console.WriteLine(bits1.FormatString());
Console.WriteLine();

Console.WriteLine($"NOT {bits1.FormatString()}");
bits1.Not();
Console.WriteLine($" =  {bits1.FormatString()}");
Console.WriteLine();

BitArray bits2 = new(bits1);
bits2[0] = true;
bits2[1] = false;
bits2[4] = true;
Console.WriteLine($"   {bits1.FormatString()}");
Console.WriteLine($"OR {bits2.FormatString()}");
bits1.Or(bits2);
Console.WriteLine($"=  {bits1.FormatString()}");
Console.WriteLine();

Console.WriteLine($"    {bits2.FormatString()}");
Console.WriteLine($"AND {bits1.FormatString()}");
bits2.And(bits1);
Console.WriteLine($"=   {bits2.FormatString()}");
Console.WriteLine();

Console.WriteLine($"    {bits1.FormatString()} ");
Console.WriteLine($"XOR {bits2.FormatString()}");
bits1.Xor(bits2);
Console.WriteLine($"=   {bits1.FormatString()}");
Console.ReadLine();
