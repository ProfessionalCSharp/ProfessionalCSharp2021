namespace StoredProceduresWithEFCore.Data;

public partial class MenuItem
{
    public int MenuItemId { get; set; }

    public string Title { get; set; } = null!;

    public string? Subtitle { get; set; }

    public decimal Price { get; set; }

    public string? KitchenInfo { get; set; }

    public int? MenusSold { get; set; }
}
