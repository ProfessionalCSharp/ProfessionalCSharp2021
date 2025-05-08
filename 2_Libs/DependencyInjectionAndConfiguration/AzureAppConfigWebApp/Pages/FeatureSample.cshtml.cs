namespace AzureAppConfigWebApp.Pages;

public class FeatureSampleModel(IFeatureManager featureManager) : PageModel
{
    public string? FeatureXText { get; private set; }

    public async Task OnGetAsync()
    {
        bool featureX = await featureManager.IsEnabledAsync("FeatureX");
        string featureText = featureX ? "is" : "is not";
        FeatureXText = $"FeatureX {featureText} available";
    }
}
