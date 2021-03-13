using Microsoft.ApplicationModel.Resources;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

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
