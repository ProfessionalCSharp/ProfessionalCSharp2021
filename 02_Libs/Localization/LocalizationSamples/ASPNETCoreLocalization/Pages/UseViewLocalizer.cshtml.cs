using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASPNETCoreLocalization.Pages
{
    public class UseViewLocalizerModel : PageModel
    {
        private IViewLocalizer _viewLocalizer;
        public UseViewLocalizerModel(IViewLocalizer viewLocalizer)
        {
            _viewLocalizer = viewLocalizer;
        }
        public void OnGet()
        {
            Text1 = _viewLocalizer["Text1"].Value;
        }
        public string? Text1 { get; private set; }
    }
}
