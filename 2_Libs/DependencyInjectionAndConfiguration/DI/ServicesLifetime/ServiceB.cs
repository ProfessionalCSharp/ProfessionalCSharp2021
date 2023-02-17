namespace DISample;

public class ConfigurationB
{
    public string? Mode { get; set; }
}

public sealed class ServiceB : IServiceB, IDisposable
{
    private readonly int _n;
    private readonly string? _mode;
    public ServiceB(INumberService numberService, IOptions<ConfigurationB> options)
    {
        _mode = options.Value.Mode;
        _n = numberService.GetNumber();
        Console.WriteLine($"ctor {nameof(ServiceB)}, {_n}");
    }

    public void B() => Console.WriteLine($"{nameof(B)}, {_n}, mode: {_mode}");
    public void Dispose() => Console.WriteLine($"disposing {nameof(ServiceB)}, {_n}");
}
