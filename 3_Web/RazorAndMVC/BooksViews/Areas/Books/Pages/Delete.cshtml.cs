using BookModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BooksViews;

public class DeleteModel : PageModel
{
    private readonly BooksContext _context;

    public DeleteModel(BooksContext context) => _context = context;

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        Book = await _context.Books.FindAsync(id);

        if (Book is not null)
        {
            _context.Books.Remove(Book);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
