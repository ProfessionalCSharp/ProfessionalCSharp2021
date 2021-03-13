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
        public MainWindow(ResourceLoader resourceLoader, ResourceManager resourceManager)
        {
            this.InitializeComponent();
            _resourceLoader = resourceLoader;
            this.Activated += OnActivated;
        }

        private void OnActivated(object sender, WindowActivatedEventArgs args)
        {
            text1.Text = DateTime.Today.ToString("D");
            text2.Text = _resourceLoader.GetString("Hello");

            //ResourceManager
            //var resourceLoader = ResourceLoader.GetDefaultResourceFilePath.GetForCurrentView("Messages");
            //text2.Text = resourceLoader.GetString("Hello");
        }


    }
}
