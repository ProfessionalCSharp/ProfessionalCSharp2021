using System;
using System.Runtime.InteropServices;

public readonly struct Currency
{
    public readonly uint Dollars;
    public readonly ushort Cents;

    public Currency(uint dollars, ushort cents)
    {
        Dollars = dollars;
        Cents = cents;
    }

    public override string ToString() => $"${Dollars}.{Cents,-2:00}";

    public static implicit operator float(Currency value) =>
        value.Dollars + (value.Cents / 100.0f);

    public static explicit operator Currency(float value)
    {
        //// version 1
        //uint dollars = (uint)value;
        //ushort cents = (ushort)((value - dollars) * 100);
        //return new Currency(dollars, cents);

        try
        {
            // version 2
            checked
            {
                uint dollars = (uint)value;

                ushort cents = Convert.ToUInt16((value - dollars) * 100);
                return new Currency(dollars, cents);
            }
        }
        catch (OverflowException ex)
        {
            throw new InvalidCastException($"invalid cast from float with the value {value}", ex);
        }
    }

    public static implicit operator Currency(uint value) => new Currency(value, 0);

    public static implicit operator uint(Currency value) => value.Dollars;
}
