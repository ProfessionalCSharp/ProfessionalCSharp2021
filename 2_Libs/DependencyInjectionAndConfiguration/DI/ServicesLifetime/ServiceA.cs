public class ConfigurationA
{
    public string? Mode { get; set; }
}

public sealed class ServiceA : IServiceA, IDisposable
{
    private readonly int _n;
    private readonly string? _mode;
    public ServiceA(INumberService numberService, IOptions<ConfigurationA> options)
    {
        _mode = options.Value.Mode;
        _n = numberService.GetNumber();
        Console.WriteLine($"ctor {nameof(ServiceA)}, {_n}");
    }

    public void A() => Console.WriteLine($"{nameof(A)}, {_n}, mode: {_mode}");
    public void Dispose() => Console.WriteLine($"disposing {nameof(ServiceA)}, {_n}");
}
