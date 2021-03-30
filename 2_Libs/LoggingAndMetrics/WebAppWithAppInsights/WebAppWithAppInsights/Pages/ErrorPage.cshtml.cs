using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebAppWithAppInsights.Pages
{
    public class ErrorPageModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public ErrorPageModel(ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _logger = loggerFactory.CreateLogger("ErrorPage");
            _configuration = configuration;
            InstrumentationKey = "Empty";
        }

        public void OnGet()
        {
            InstrumentationKey = _configuration.GetSection("ApplicationInsights")["InstrumentationKey"];
            _logger.LogError("Error occurred in ErrorPage");
        }

        public string? InstrumentationKey { get; set; }
    }

}
