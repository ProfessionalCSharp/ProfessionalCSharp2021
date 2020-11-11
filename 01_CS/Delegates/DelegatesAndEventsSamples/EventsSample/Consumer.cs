using System;

namespace Wrox.ProCSharp.Delegates
{
    public record Consumer(string Name)
    {
        public void NewCarIsHere(object? sender, CarInfoEventArgs e) =>
          Console.WriteLine($"{Name}: car {e.Car} is new");
    }
}
