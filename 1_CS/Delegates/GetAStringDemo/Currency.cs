struct Currency(uint dollars, ushort cents)
{
    public uint Dollars = dollars;
    public ushort Cents = cents;

    public override readonly string ToString() => $"${Dollars}.{Cents,2:00}";

    public static string GetCurrencyUnit() => "Dollar";

    public static explicit operator Currency(float value)
    {
        checked
        {
            uint dollars = (uint)value;
            ushort cents = (ushort)((value - dollars) * 100);
            return new Currency(dollars, cents);
        }
    }

    public static implicit operator float(Currency value) =>
      value.Dollars + (value.Cents / 100.0f);

    public static implicit operator Currency(uint value) =>
      new(value, 0);

    public static implicit operator uint(Currency value) =>
      value.Dollars;
}