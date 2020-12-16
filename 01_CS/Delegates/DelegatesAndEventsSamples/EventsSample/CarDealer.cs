using System;

public class CarInfoEventArgs : EventArgs
{
    public CarInfoEventArgs(string car) => Car = car;

    public string Car { get; }
}

public class CarDealer
{
    public event EventHandler<CarInfoEventArgs>? NewCarCreated;

    public void CreateANewCar(string car)
    {
        Console.WriteLine($"CarDealer, new car {car}");

        RaiseNewCarCreated(car);
    }

    private void RaiseNewCarCreated(string car) =>
        NewCarCreated?.Invoke(this, new CarInfoEventArgs(car));
}
