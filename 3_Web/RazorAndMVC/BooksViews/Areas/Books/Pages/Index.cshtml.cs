using BookModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BooksViews;

public class IndexModel : PageModel
{
    private readonly BooksContext _context;

    public IndexModel(BooksContext context) => _context = context;

    public IList<Book>? Books { get;set; }

    public async Task OnGetAsync()
    {
        Books = await _context.Books.ToListAsync();
    }
}
