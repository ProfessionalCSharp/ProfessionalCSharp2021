// pass int by reference
int a = 1;
ChangeAValueType(ref a);
Console.WriteLine($"the value of a changed to {a}");

// pass a reference by reference
SomeData data1 = new() { Value = 1 };
ChangingAReferenceByRef(ref data1);
Console.WriteLine($"the new value of data1.Value is: {data1.Value}");

// use the in modifier to pass by reference
SomeValue myData = new(1, 2, 3, 4);
PassValueByReferenceReadonly(in myData);

// use the out keyword
Console.Write("Please enter a number: ");
string? input = Console.ReadLine();
if (int.TryParse(input, out int x))
{
    Console.WriteLine();
    Console.WriteLine($"read an int: {x}");
}

// use ref return

SomeValue one = new SomeValue(1, 2, 3, 4);
SomeValue two = new SomeValue(5, 6, 7, 8);

SomeValue bigger1 = Max(ref one, ref two);
ref SomeValue bigger2 = ref Max(ref one, ref two);

ref readonly SomeValue bigger3 = ref Max(ref one, ref two);

ref readonly SomeValue bigger4 = ref MaxReadonly(in one, in two);
SomeValue bigger5 = MaxReadonly(in one, in two);

ref SomeValue Max(ref SomeValue x, ref SomeValue y)
{
    int sumx = x.Value1 + x.Value2 + x.Value3 + x.Value4;
    int sumy = y.Value1 + y.Value2 + y.Value3 + y.Value4;

    //if (sumx > sumy)
    //{
    //    return ref x;
    //}
    //else
    //{
    //    return ref y;
    //}

    ref SomeValue r = ref (sumx > sumy) ? ref x : ref y;
    return ref r;
}

ref readonly SomeValue MaxReadonly(in SomeValue x, in SomeValue y)
{
    int sumx = x.Value1 + x.Value2 + x.Value3 + x.Value4;
    int sumy = y.Value1 + y.Value2 + y.Value3 + y.Value4;

    return ref (sumx > sumy) ? ref x : ref y;
}


void PassValueByReferenceReadonly(in SomeValue data)
{
    // data.Value1 = 4; - you cannot change a value, it's a read-only variable!
}

void ChangeAValueType(ref int x)
{
    x = 2;
}

void ChangingAReferenceByRef(ref SomeData data)
{
    data.Value = 2;
    data = new SomeData { Value = 3 };
}

class SomeData
{
    public int Value { get; set; }
}

struct SomeValue
{
    public SomeValue(int value1, int value2, int value3, int value4)
    {
        Value1 = value1;
        Value2 = value2;
        Value3 = value3;
        Value4 = value4;
    }
    public int Value1 { get; set; }
    public int Value2 { get; set; }
    public int Value3 { get; set; }
    public int Value4 { get; set; }
}
