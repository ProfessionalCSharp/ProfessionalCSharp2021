using System.Numerics;

int[] list1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
double[] list2 = { 1, 2, 3, 4.4, 5, 6, 7, 8, 9 };

var result = AddAll1(list1);
Console.WriteLine(result);

var result2 = AddAll2(list2);
Console.WriteLine(result2);

var result3 = AddAll3(list2);
Console.WriteLine(result3);

var result4 = AddAll4(list2.AsSpan());
Console.WriteLine(result4);

int AddAll1(int[] values)
{
    int result = 0;
    foreach (var value in values)
    {
        result += value;
    }
    return result;
}

// with INumber<T> constraint!
T AddAll2<T>(T[] values) where T : INumber<T>
{
    T result = T.Zero;
    foreach (var value in values)
    {
        result += value;
    }
    return result;
}

// with list pattern matching - and INumber<T> constraint
T AddAll3<T>(T[] values) where T : INumber<T> =>
    values switch
    {
        [] => T.Zero,
        [var first, .. var rest] => first + AddAll3(rest),
    };

T AddAll4<T>(Span<T> values) where T : INumber<T> =>
    values switch
    {
        [] => T.Zero,
        [var first, .. var rest] => first + AddAll4(rest),
    };
