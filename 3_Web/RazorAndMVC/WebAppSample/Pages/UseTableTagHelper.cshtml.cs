using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using WebAppSample.Models;

namespace WebAppSample.Pages
{
    public class UseTableTagHelperModel : PageModel
    {
        public UseTableTagHelperModel(MenuSamplesService menuSampleService) => MenuItems = menuSampleService.GetMenuItems();

        public IEnumerable<MenuItem> MenuItems { get; }
        public void OnGet()
        {
        }
    }
}
