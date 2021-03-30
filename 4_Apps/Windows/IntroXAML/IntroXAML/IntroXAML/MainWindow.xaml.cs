using DataLib;
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
using Windows.UI.Popups;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace IntroXAML
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            var button2 = new Button
            {
                Content = "created dynamically"
            };
            button2.Click += async (sender, e) => await new MessageDialog("button 2 clicked").ShowAsync();
            stackPanel1.Children.Add(button2);

            list1.Items.Add(new Person { FirstName = "Stephanie", LastName = "Nagel" });
        }

        private async void OnButtonClick(object sender, RoutedEventArgs e)
        {
            await new MessageDialog("button 1 clicked").ShowAsync();
        }
    }
}
