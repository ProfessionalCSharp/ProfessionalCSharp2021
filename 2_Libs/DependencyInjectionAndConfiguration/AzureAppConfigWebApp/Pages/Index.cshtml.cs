namespace AzureAppConfigWebApp.Pages;
public class IndexModel(IOptionsSnapshot<IndexAppSettings> options, ILogger<IndexModel> logger, IConfiguration configuration) : PageModel
{
    public string Config1 { get; } = options.Value.Config1 ?? "no value";

    public void OnGet()
    {

    }
}
