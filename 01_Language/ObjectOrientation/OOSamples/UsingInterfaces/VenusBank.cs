using System;

namespace UsingInterfaces
{
    public class SaverAccount : IBankAccount
    {
        public void PayIn(decimal amount) => Balance += amount;

        public bool Withdraw(decimal amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
                return true;
            }
            Console.WriteLine("Withdrawal attempt failed.");
            return false;
        }

        public decimal Balance { get; private set; }

        public override string ToString() =>
            $"Venus Bank Saver: Balance = {Balance,6:C}";
    }
}