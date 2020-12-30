public class InnerConfig
{
    public string? Key5 { get; set; }
}

public class StronglyTypedConfig
{
    public string? Key3 { get; set; }
    public string? Key4 { get; set; }
    public InnerConfig? InnerConfig { get; set; }

    public override string ToString() =>
        $"values: {Key3} {Key4} {InnerConfig?.Key5}";
}
