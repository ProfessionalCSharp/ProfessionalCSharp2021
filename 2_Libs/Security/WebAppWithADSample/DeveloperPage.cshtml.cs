using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppWithADSample
{
    [Authorize(Policy="Developers")]
    public class DeveloperPageModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
