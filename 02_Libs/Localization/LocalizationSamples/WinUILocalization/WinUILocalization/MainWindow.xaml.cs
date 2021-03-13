using Microsoft.ApplicationModel.Resources;
using Microsoft.UI.Xaml;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUILocalization
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
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

            this.InitializeComponent();
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
}
