class GenericMethods
{
    public static void Swap<T>(ref T x, ref T y)
    {
        T temp;
        temp = x;
        x = y;
        y = temp;
    }
}

