using System.Collections.Generic;

public class MenuCard
{
    public MenuCard(string title, int menuCardId = default) 
        => (Title, MenuCardId) = (title, menuCardId);

    public int MenuCardId { get; set; }
    public string Title { get; set; }
    public ICollection<MenuItem> Menus { get; } = new List<MenuItem>();
    public override string ToString() => Title;
}
