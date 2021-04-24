using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppSample.Pages
{
    public class UseViewComponentModel : PageModel
    {
        public bool ShowDates { get; set; } = false;

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DateSelectionViewModel? DateSelection { get; set; } = new DateSelectionViewModel();

        public IActionResult OnPost()
        {
            ShowDates = true;
            return Page();
        }
    }

    public class DateSelectionViewModel
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
