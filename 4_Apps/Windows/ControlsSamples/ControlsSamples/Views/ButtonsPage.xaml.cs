using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using System;
using System.Runtime.InteropServices;
using Windows.UI.Popups;
using WinRT;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ControlsSamples.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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
