using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;

namespace ASPNETCoreLocalization.Pages
{
    public class UseResourceModel : PageModel
    {
        private readonly IStringLocalizer _localizer;
        private readonly IStringLocalizer _sharedLocalizer;
        public UseResourceModel(IStringLocalizer<UseResourceModel> localizer, IStringLocalizer<Startup> sharedLocalizer)
        {
            _localizer = localizer;
            _sharedLocalizer = sharedLocalizer;
        }

        public void OnGet()
        {
            var feature = HttpContext.Features.Get<IRequestCultureFeature>();
            RequestCulture requestCulture = feature.RequestCulture;
            Message1 = _localizer["Message1"];
            Message2 = _localizer.GetString("Message2", feature.RequestCulture.Culture, feature.RequestCulture.UICulture);
            Message3 = _sharedLocalizer.GetString("SharedText");
        }

        public string? Message1 { get; private set; }
        public string? Message2 { get; private set; }
        public string? Message3 { get; private set; }
    }
}
