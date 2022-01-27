using BookModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BooksViews;

public class DetailsModel : PageModel
{
    private readonly BooksContext _context;

    public DetailsModel(BooksContext context) => _context = context;

    public Book? Book { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Book = await _context.Books.FirstOrDefaultAsync(m => m.BookId == id);

        if (Book == null)
        {
            return NotFound();
        }
        return Page();
    }
}
