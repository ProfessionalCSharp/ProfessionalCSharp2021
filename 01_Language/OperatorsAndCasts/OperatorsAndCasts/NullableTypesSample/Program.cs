using System;

#pragma warning disable CS0219 // Variable is assigned but its value is never used
int i1 = 1;

int? i2 = 2;
int? i3 = null;

long? l1 = null;
DateTime? d1 = null;

int? a = null;
int? b = a + 4; // b = null
int? c = a * 5; // c = null

#pragma warning restore CS0219 // Variable is assigned but its value is never used

IfCompareToNull();

static void IfCompareToNull()
{
    int? a = null;
    int? b = -5;
    if (a >= b) // if a or b is null, this condition is false
    {
        Console.WriteLine("a >= b");
    }
    else
    {
        Console.WriteLine("a < b");
                
    }
}
