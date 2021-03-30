public class Menu
{
    public Menu(string title, int menuId = 0)
    {
        Title = title;
        MenuId = menuId;
    }
    public int MenuId { get; set; }
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
    public Menu? Menu { get; set; }
}
