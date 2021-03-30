using System;

public class Menu
{
    public Menu(string text, Guid menuId = default) => (Text, MenuId) = (text, menuId);

    public Guid MenuId { get; set; }
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
