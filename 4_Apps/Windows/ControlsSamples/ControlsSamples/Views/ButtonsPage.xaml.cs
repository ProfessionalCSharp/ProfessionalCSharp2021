using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using System.Runtime.InteropServices;

using Windows.UI.Popups;

using WinRT;

namespace ControlsSamples.Views
{
    public sealed partial class ButtonsPage : Page
    {
        public ButtonsPage() => this.InitializeComponent();

        private async void OnButtonClick(object sender, RoutedEventArgs e)
        {
            // TODO: change to (with version 0.8):
            // await new MessageDialog("Button clicked").ShowAsync();           
            MessageDialog dlg = new("button 1 clicked");
            var handle = GetActiveWindow();
            if (handle == IntPtr.Zero)
                throw new InvalidOperationException();
            dlg.As<IInitializeWithWindow>().Initialize(handle);
            await dlg.ShowAsync();
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("3E68D4BD-7135-4D10-8018-9FB6D9F33FA1")]
        internal interface IInitializeWithWindow
        {
            void Initialize(IntPtr hwnd);
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetActiveWindow();
    }
}
