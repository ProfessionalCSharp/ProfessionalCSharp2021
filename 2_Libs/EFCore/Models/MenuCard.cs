using System.Collections.Generic;

public class MenuCard
{
    public MenuCard(string title, int menuCardId = default) 
        => (Title, MenuCardId) = (title, menuCardId);

    public int MenuCardId { get; set; }
    public string Title { get; set; }
    public ICollection<Menu> Menus { get; } = new List<Menu>();
    public override string ToString() => Title;
}
