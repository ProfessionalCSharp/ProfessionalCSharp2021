using DataLib;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
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
            Button button2 = new()
            {
                Content = "created dynamically"
            };
            button2.Click += async (sender, e) =>
            {
                MessageDialog dlg = new("button 2 clicked");
                await dlg.ShowAsync();
            };
            stackPanel1.Children.Add(button2);

            list1.Items.Add(new Person() { FirstName = "Stephanie", LastName = "Nagel" });
        }

        private async void OnButtonClick(object sender, RoutedEventArgs e)
        {
            await new MessageDialog("button 1 clicked").ShowAsync();
        }
    }
}
