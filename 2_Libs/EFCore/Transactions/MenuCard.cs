using System;
using System.Collections.Generic;

public class MenuCard
{
    public MenuCard(string title, Guid menuCardId = default) 
        => (Title, MenuCardId) = (title, menuCardId);

    public Guid MenuCardId { get; set; }
    public string Title { get; set; }
    public ICollection<Menu> Menus { get; } = new List<Menu>();
    public override string ToString() => Title;
}
