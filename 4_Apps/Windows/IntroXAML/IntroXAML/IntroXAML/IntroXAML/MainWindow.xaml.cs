using DataLib;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Windows.UI.Popups;
using WinRT;

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
                // TODO: change to (with version 0.8):
                // await new MessageDialog("button 2 clicked").ShowAsync();
                MessageDialog dlg = new("button 2 clicked");

                IInitializeWithWindow withWindow = dlg.As<IInitializeWithWindow>();
                var handle = this.As<IWindowNative>().WindowHandle;
                try
                {
                    withWindow.Initialize(handle);
                    await dlg.ShowAsync();
                }
                catch (System.Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            };
            stackPanel1.Children.Add(button2);

            list1.Items.Add(new Person() { FirstName = "Stephanie", LastName = "Nagel" });
        }

        private async void OnButtonClick(object sender, RoutedEventArgs e)
        {
            // TODO: change to (with version 0.8):
            // await new MessageDialog("button 1 clicked").ShowAsync();

            MessageDialog dlg = new("button 1 clicked");

            IInitializeWithWindow withWindow = dlg.As<IInitializeWithWindow>();
            var handle = this.As<IWindowNative>().WindowHandle;
            try
            {
                withWindow.Initialize(handle);
                await dlg.ShowAsync();
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("3E68D4BD-7135-4D10-8018-9FB6D9F33FA1")]
    internal interface IInitializeWithWindow
    {
        void Initialize(IntPtr hwnd);
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("EECDBF0E-BAE9-4CB6-A68E-9598E1CB57BB")]
    internal interface IWindowNative
    {
        IntPtr WindowHandle { get; }
    }
}
