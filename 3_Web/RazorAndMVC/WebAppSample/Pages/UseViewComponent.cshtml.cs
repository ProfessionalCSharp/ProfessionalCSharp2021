using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace WebAppSample.Pages
{
    public class UseViewComponentModel : PageModel
    {
        public bool ShowEvents { get; set; } = false;

        public IActionResult OnGet() => Page();

        [BindProperty]
        public DateSelectionViewModel DateSelection { get; set; } = new DateSelectionViewModel();

        public IActionResult OnPost()
        {
            ShowEvents = true;
            return Page();
        }
    }

    public class DateSelectionViewModel
    {
        public DateTime From { get; set; } = DateTime.Today;
        public DateTime To { get; set; } = DateTime.Today.AddDays(20);
    }
}
