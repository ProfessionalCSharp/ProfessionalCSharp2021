using Microsoft.UI.Xaml;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Popups;
using WinRT;

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
                result += $"\nasync function after await : {GetThread()}; ";

                try
                {
                    text1.Text = "this is a call from the wrong thread";
                    return "not reached";
                }
                catch (Exception ex) when (ex.HResult == -2147417842)
                {
                    result += $"exception: {ex.Message}";
                    return result;
                    // we know it's the wrong thread
                    // don't access UI elements from the previous try block
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
            MessageDialog dlg = new("Select One, Two, Or Three", "Sample");
            // TODO: remove workaround after next preview?

            IInitializeWithWindow withWindow = dlg.As<IInitializeWithWindow>();
            var handle = this.As<IWindowNative>().WindowHandle;
            try
            {
                dlg.Commands.Add(new UICommand("One", null, 1));
                dlg.Commands.Add(new UICommand("Two", null, 2));
                dlg.Commands.Add(new UICommand("Three", null, 3));

                withWindow.Initialize(handle);
                IUICommand command = await dlg.ShowAsync();

                text1.Text = $"Command {command.Id} with the label {command.Label} invoked";
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void OnStartDeadlock(object sender, RoutedEventArgs e)
        {
            // invoke DelayAsync and block the UI thread until completed
            DelayAsync().Wait();

            async Task DelayAsync()
            {
                // DelayAsync is started, Task.Delay returns the task - now start the blocking Wait above and wait until it is completed
                await Task.Delay(1000); // .ConfigureAwait(false);  deadlock without ConfigureAwait
                // the UI thread cannot continue here, because it is waiting above
            }
        }

        private string GetThread() => $"thread: {Thread.CurrentThread.ManagedThreadId}";

        // TODO: remove workaround with next preview?
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
}
