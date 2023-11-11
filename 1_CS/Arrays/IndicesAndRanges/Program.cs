using IndicesAndRanges;

int[] data = [1, 2, 3, 4, 5, 6, 7, 8, 9];

// indices and the hat operator

int first1 = data[0];
int last1 = data[data.Length - 1];
Console.WriteLine($"first: {first1}, last: {last1}");

int last2 = data[^1];
Console.WriteLine(last2);

Index firstIndex = 0;
Index lastIndex = ^1;
int first3 = data[firstIndex];
int last3 = data[lastIndex];
Console.WriteLine($"first: {first3}, last: {last3}");

// ranges

ShowRange("full range", data[..]);
ShowRange("first three", data[0..3]);
ShowRange("fourth to sixth", data[3..6]);
ShowRange("last three", data[^3..^0]);

static void ShowRange(string title, int[] data)
{
    Console.WriteLine(title);
    Console.WriteLine(string.Join(" ", data));
    Console.WriteLine();
}

Range fullRange = ..;
Range firstThree = 0..3;
Range fourthToSixth = 3..6;
Range lastThree = ^3..^0;

Console.WriteLine(fullRange);

// efficiently changing array content

int[] slice1 = data[3..5];
slice1[0] = 42;
Console.WriteLine($"value in array didn't change: {data[3]}, value from slice: {slice1[0]}");

var sliceToSpan = data.AsSpan()[3..5];
sliceToSpan[0] = 42;
Console.WriteLine($"value in array: {data[3]}, value from slice: {sliceToSpan[0]}");

// indices and ranges with custom collections

MyCollection coll = new();
int n = coll[^20];
Console.WriteLine($"Item from the collection: {n}");
ShowRange("Using custom collection", coll[45..^40]);
