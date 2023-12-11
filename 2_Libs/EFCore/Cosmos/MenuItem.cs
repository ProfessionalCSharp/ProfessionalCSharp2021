public class MenuItem(string text, Guid menuItemId = default)
{
    public Guid MenuItemId { get; set; } = menuItemId;
    public string Text { get; set; } = text;
    public decimal? Price { get; set; }

    public override string ToString() => Text;
}
