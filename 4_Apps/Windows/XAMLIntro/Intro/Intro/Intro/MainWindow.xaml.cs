using DataLib;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using Windows.UI.Popups;

using WinRT.Interop;

namespace IntroXAML;

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
            ContentDialog dlg = new()
            {
                Title = "Message",
                Content = "button 2 clicked",
                PrimaryButtonText = "OK",
                XamlRoot = this.Content.XamlRoot
            };

            await dlg.ShowAsync();
        };

        stackPanel1.Children.Add(button2);

        list1.Items.Add(new Person() { FirstName = "Stephanie", LastName = "Nagel" });
    }

    private async void OnButtonClick(object sender, RoutedEventArgs e)
    {
        ContentDialog dlg = new()
        {
            Title = "Message",
            Content = "button 1 clicked",
            PrimaryButtonText = "OK",
            XamlRoot = this.Content.XamlRoot
        };
        await dlg.ShowAsync();
    }
}
