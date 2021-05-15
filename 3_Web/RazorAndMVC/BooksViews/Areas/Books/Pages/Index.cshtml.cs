using BookModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksViews
{
    public class IndexModel : PageModel
    {
        private readonly BookModels.BooksContext _context;

        public IndexModel(BookModels.BooksContext context)
        {
            _context = context;
        }

        public IList<Book>? Books { get;set; }

        public async Task OnGetAsync()
        {
            Books = await _context.Books.ToListAsync();
        }
    }
}
