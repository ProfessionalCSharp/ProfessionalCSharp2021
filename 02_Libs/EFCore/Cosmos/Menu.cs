using System;

public class Menu
{
    public Menu(string text, Guid menuId = default) => (Text, MenuId) = (text, menuId);

    public Guid MenuId { get; set; }
    public string Text { get; set; }
    public decimal? Price { get; set; }
    public override string ToString() => Text;
}
