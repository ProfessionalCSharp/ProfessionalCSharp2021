using Microsoft.UI.Xaml;
using Microsoft.Windows.ApplicationModel.Resources;

namespace WinUILocalization;

public sealed partial class MainWindow : Window
{
    private readonly ResourceLoader _resourceLoader;
    private readonly ResourceManager _resourceManager;
    private readonly ResourceContext _resourceContext;
    public MainWindow(ResourceLoader resourceLoader, ResourceManager resourceManager)
    {
        _resourceLoader = resourceLoader;
        _resourceManager = resourceManager;
        _resourceContext = _resourceManager.CreateResourceContext();
        _resourceContext.QualifierValues["language"] = "de";

        InitializeComponent();
    }

    private void OnGetResource(object sender, RoutedEventArgs e)
    {
        textDate.Text = DateTime.Today.ToString("D");
        textHello.Text = _resourceLoader.GetString("Hello");
    }

    private void OnUseResourceManager(object sender, RoutedEventArgs e)
    {
        ResourceMap map = _resourceManager.MainResourceMap;

        ResourceCandidate candidate = map.TryGetValue("Resources/GoodMorning");
        textGoodMorning.Text = candidate.ValueAsString;
    }

    private void OnUseContext(object sender, RoutedEventArgs e)
    {
        ResourceMap map = _resourceManager.MainResourceMap;
        ResourceCandidate candidate = map.TryGetValue("Resources/GoodEvening", _resourceContext);
        textGoodEvening.Text = candidate.ValueAsString;
    }
}
