public class MenuCard
{
    public MenuCard(string title, Guid menuCardId = default) 
        => (Title, MenuCardId) = (title, menuCardId);

    public Guid MenuCardId { get; set; }
    public string Title { get; set; }
    public ICollection<MenuItem> MenuItems { get; } = new List<MenuItem>();
    public override string ToString() => Title;
}
