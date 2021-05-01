using System;

public class MenuItem
{
    public MenuItem(string text, Guid menuItemId = default) => (Text, MenuItemId) = (text, menuItemId);

    public Guid MenuItemId { get; set; }
    public string Text { get; set; }
    public decimal? Price { get; set; }
    private MenuCard? _menuCard;
    public MenuCard MenuCard
    {
        get => _menuCard ?? throw new InvalidOperationException($"{nameof(MenuCard)} not initialized");
        init => _menuCard = value;
    }
    public override string ToString() => Text;
}
