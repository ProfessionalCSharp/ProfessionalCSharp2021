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
            MessageDialog dlg = new("button 1 clicked");

            IntPtr hwnd = WindowNative.GetWindowHandle(this);
            InitializeWithWindow.Initialize(dlg, hwnd);
            await dlg.ShowAsync();
        }
    }
}
