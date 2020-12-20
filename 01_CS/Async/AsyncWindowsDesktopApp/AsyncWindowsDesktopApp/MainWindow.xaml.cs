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
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AsyncWindowsDesktopApp
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private async void OnStartAsync(object sender, RoutedEventArgs e)
        {

            text1.Text = $"UI thread: {GetThread()}";

            await Task.Delay(1000);

            text1.Text += $"\nafter await: {GetThread()}";
        }

        private async void OnStartAsyncConfigureAwait(object sender, RoutedEventArgs e)
        {
            text1.Text = $"UI thread: {GetThread()}";

            string s = await AsyncFunction().ConfigureAwait(continueOnCapturedContext: true);

            // after await, with continueOnCapturedContext true we are back in the UI thread
            text1.Text += $"\n{s}\nafter await: {GetThread()}";

            async Task<string> AsyncFunction()
            {
                string result = $"\nasync function: {GetThread()}\n";
                await Task.Delay(1000).ConfigureAwait(continueOnCapturedContext: false);
                result += $"\nasync function after await : {GetThread()}";

                try
                {
                    text1.Text = "this is a call from the wrong thread";
                    return "not reached";
                }
                catch (Exception ex) when (ex.HResult == -2147417842)
                {
                    return result;
                    // we know it's the wrong thread, so don't access
                    // UI elements from the previous try block
                }
            }
        }

        private async void OnStartAsyncWithThreadSwitch(object sender, RoutedEventArgs e)
        {
            text1.Text = $"UI thread: {GetThread()}";

            string s = await AsyncFunction();

            text1.Text += $"\n{s}\nafter await: {GetThread()}";

            async Task<string> AsyncFunction()
            {
                string result = $"\nasync function: {GetThread()}\n";
                await Task.Delay(1000).ConfigureAwait(continueOnCapturedContext: false);
                result += $"\nasync function after await : {GetThread()}";

                await text1.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    text1.Text += $"\nasync function switch back to the UI thread: {GetThread()}";
                });

                return result;
            }
        }

        private async void OnIAsyncOperation(object sender, RoutedEventArgs e)
        {
            var dlg = new MessageDialog("Select One, Two, Or Three", "Sample");

            dlg.Commands.Add(new UICommand("One", null, 1));
            dlg.Commands.Add(new UICommand("Two", null, 2));
            dlg.Commands.Add(new UICommand("Three", null, 3));

            IUICommand command = await dlg.ShowAsync();

            text1.Text = $"Command {command.Id} with the label {command.Label} invoked";
        }

        private void OnStartDeadlock(object sender, RoutedEventArgs e)
        {
            DelayAsync().Wait();

            async Task DelayAsync()
            {
                await Task.Delay(1000);
            }
        }

        private string GetThread() => $"thread: {Environment.CurrentManagedThreadId}";
    }
}
