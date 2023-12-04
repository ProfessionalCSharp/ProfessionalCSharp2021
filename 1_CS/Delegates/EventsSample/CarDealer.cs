public class CarInfoEventArgs(string car) : EventArgs
{
    public string Car { get; } = car;
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
