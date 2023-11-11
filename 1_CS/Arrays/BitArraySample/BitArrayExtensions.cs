namespace BitArraySample;

public static class BitArrayExtensions
{
    public static string FormatString(this BitArray bits)
    {
        StringBuilder sb = new();
        for (int i = bits.Length - 1; i >= 0; i--)
        {
            sb.Append(bits[i] ? 1 : 0);
            if (i != 0 && i % 4 == 0)
            {
                sb.Append('_');
            }
        }

        return sb.ToString();
    }
}

