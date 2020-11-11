using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AzureAppConfigWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IOptionsSnapshot<IndexAppSettings> options, ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            Config1 = options.Value.Config1 ?? "no value";
        }

        public string Config1 { get; }

        public void OnGet()
        {

        }
    }
}
