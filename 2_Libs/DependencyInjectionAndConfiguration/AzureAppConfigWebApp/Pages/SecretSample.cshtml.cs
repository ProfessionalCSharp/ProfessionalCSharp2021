namespace AzureAppConfigWebApp.Pages;
public class SecretSampleModel : PageModel
{
    private readonly IConfiguration _configuration;
    public SecretSampleModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string? Secret1 { get; private set; }

    public void OnGet()
    {
        Secret1 = _configuration.GetSection("")["Secret1"];
    }
}
