public class MenuCard(string title, Guid menuCardId = default)
{
    public Guid MenuCardId { get; set; } = menuCardId;
    public string Title { get; set; } = title;
    public ICollection<MenuItem> MenuItems { get; } = [];
    public override string ToString() => Title;
}
