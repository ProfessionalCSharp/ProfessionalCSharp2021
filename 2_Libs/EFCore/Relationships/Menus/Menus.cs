public class MenuItem
{
    public MenuItem(string title, int menuItemId = 0)
    {
        Title = title;
        MenuItemId = menuItemId;
    }
    public int MenuItemId { get; set; }
    public string Title { get; set; }
    public string? Subtitle { get; set; }
    public decimal Price { get; set; }
    public MenuDetails? Details { get; set; }
}

public class MenuDetails
{
    public int MenuDetailsId { get; set; }
    public string? KitchenInfo { get; set; }
    public int MenusSold { get; set; }
    public MenuItem? Menu { get; set; }
}
