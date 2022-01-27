#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppWithAppInsights.Data;

namespace WebAppWithAppInsights.Areas.Books
{
    public class IndexModel : PageModel
    {
        private readonly WebAppWithAppInsights.Data.BooksContext _context;

        public IndexModel(WebAppWithAppInsights.Data.BooksContext context)
        {
            _context = context;
        }

        public IList<Book> Book { get;set; }

        public async Task OnGetAsync()
        {
            Book = await _context.Books.ToListAsync();
        }
    }
}
