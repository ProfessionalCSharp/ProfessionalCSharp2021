namespace TransactionsSamples;

public class MenuItem(string text, Guid menuItemId = default)
{
    public Guid MenuItemId { get; set; } = menuItemId;
    public string Text { get; set; } = text;
    public decimal? Price { get; set; }
    public Guid MenuCardId { get; internal set; }
    private MenuCard? _menuCard;
    public MenuCard MenuCard
    {
        get => _menuCard ?? throw new InvalidOperationException($"{nameof(MenuCard)} not initialized");
        init => _menuCard = value;
    }
    public override string ToString() => Text;
}
