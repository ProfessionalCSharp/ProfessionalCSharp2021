using System;

byte byte1 = byte.MaxValue;


long l1 = 0x_123_4567_89ab_cedf;
long l2 = 0x123456789abcedf;
Console.WriteLine($"maximum value of a long: {long.MaxValue}, hex: {long.MaxValue:x}");
Console.WriteLine(l1);
Console.WriteLine(l1.ToString("x"));
Console.WriteLine($"{l1:x}");

uint binary1 = 0b_1111_1110_1101_1100_1011_1010_1001_1000;
Console.WriteLine($"{binary1:X}");

// floating point values

Half half1 = Half.MaxValue;
Half half2 = (Half)3.4;

decimal d1 = decimal.Parse("3427342784378283428");
decimal d2 = new decimal(10, 0, 0, false, 10);

int[] bits = decimal.GetBits(d1);
Console.ReadLine();