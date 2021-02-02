namespace PointerPlayground2
{
    internal readonly struct CurrencyStruct
    {
        public CurrencyStruct(long dollars, byte cents) => (Dollars, Cents) = (dollars, cents);

        public readonly long Dollars;
        public readonly byte Cents;

        public override string ToString() => $"$ {Dollars}.{Cents}";
    }

    internal class CurrencyClass
    {
        public CurrencyClass(long dollars, byte cents) => (Dollars, Cents) = (dollars, cents);

        public readonly long Dollars = 0;
        public readonly byte Cents = 0;

        public override string ToString() => $"$ {Dollars}.{Cents}";
    }
}
