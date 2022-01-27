namespace AzureAppConfigWebApp.Pages;

public class FeatureSampleModel : PageModel
{
    private readonly IFeatureManager _featureManager;
    public FeatureSampleModel(IFeatureManager featureManager)
    {
        _featureManager = featureManager;
    }

    public string? FeatureXText { get; private set; }

    public async Task OnGetAsync()
    {
        bool featureX = await _featureManager.IsEnabledAsync("FeatureX");
        string featureText = featureX ? "is" : "is not";
        FeatureXText = $"FeatureX {featureText} available";
    }
}
