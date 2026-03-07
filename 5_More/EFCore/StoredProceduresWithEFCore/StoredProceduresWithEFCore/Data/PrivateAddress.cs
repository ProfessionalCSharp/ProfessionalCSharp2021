namespace StoredProceduresWithEFCore.Data;

public partial class PrivateAddress
{
    public int PersonId { get; set; }

    public string? LineOne { get; set; }

    public string? LineTwo { get; set; }

    public string? Country { get; set; }

    public string? City { get; set; }

    public virtual Person Person { get; set; } = null!;
}
