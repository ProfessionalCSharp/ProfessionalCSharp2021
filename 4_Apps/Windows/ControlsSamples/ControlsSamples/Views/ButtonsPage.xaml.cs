using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using Windows.UI.Popups;

using WinRT.Interop;

namespace ControlsSamples.Views
{
    public sealed partial class ButtonsPage : Page
    {
        public ButtonsPage() => this.InitializeComponent();

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
}
