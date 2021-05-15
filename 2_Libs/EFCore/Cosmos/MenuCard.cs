using System;
using System.Collections.Generic;

public class MenuCard
{
    public MenuCard(string title, string restaurantId, Guid menuCardId = default) 
        => (Title, RestaurantId, MenuCardId) = (title, restaurantId, menuCardId);

    public Guid MenuCardId { get; set; }
    public string Title { get; set; }
    public ICollection<MenuItem> MenuItems { get; internal set; } = new HashSet<MenuItem>();
    public string RestaurantId { get; set; }
    public bool IsActive { get; set; } = true;
    public override string ToString() => Title;
}
