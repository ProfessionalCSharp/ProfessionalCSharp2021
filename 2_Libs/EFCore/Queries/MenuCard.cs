public class MenuCard(string title, int menuCardId = default)
{
    public int MenuCardId { get; set; } = menuCardId;
    public string Title { get; set; } = title;
    public ICollection<MenuItem> MenuItems { get; } = new List<MenuItem>();
    public override string ToString() => Title;
}
