public class MenuItem
{
    public MenuItem(string text, Guid menuItemId = default) => (Text, MenuItemId) = (text, menuItemId);

    public Guid MenuItemId { get; set; }
    public string Text { get; set; }
    public decimal? Price { get; set; }

    public override string ToString() => Text;
}
