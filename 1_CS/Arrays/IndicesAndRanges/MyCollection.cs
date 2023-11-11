namespace IndicesAndRanges;

public class MyCollection
{
    private readonly int[] _array = Enumerable.Range(1, 100).ToArray();
    
    public int Length => _array.Length;

    public int this[int index]
    {
        get => _array[index];
        set => _array[index] = value;
    }

    public int[] Slice(int start, int length)
    {
        int[] slice = new int[length];
        Array.Copy(_array, start, slice, 0, length);
        return slice;
    }
}
