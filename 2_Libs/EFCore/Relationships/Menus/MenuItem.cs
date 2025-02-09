﻿public class MenuItem(string title, int menuItemId = 0)
{
    public int MenuItemId { get; init; } = menuItemId;
    public string Title { get; set; } = title;
    public string? Subtitle { get; set; }
    public decimal Price { get; set; }
    public MenuDetails? Details { get; set; }
}

public class MenuDetails
{
    public int MenuDetailsId { get; init; }
    public string? KitchenInfo { get; set; }
    public int MenusSold { get; set; }
    public MenuItem? MenuItem { get; set; }
}
