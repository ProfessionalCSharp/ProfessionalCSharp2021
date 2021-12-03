namespace ASPNETCoreLocalization.Pages;

public class EditBookModel : PageModel
{
    public void OnGet()
    {
        Book = new Book("Professional C#", "Wrox Press");
    }

    public Book? Book { get; set; }
}
