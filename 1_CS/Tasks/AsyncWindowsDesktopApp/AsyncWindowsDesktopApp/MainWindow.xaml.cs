using Microsoft.UI.Xaml;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Popups;
using WinRT;
using WinRT.Interop;

namespace AsyncWindowsDesktopApp;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
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

            text1.DispatcherQueue.TryEnqueue(() =>
            {
                text1.Text += $"\nasync function switch back to the UI thread: {GetThread()}";
            });

            return result;
        }
    }

    private async void OnIAsyncOperation(object sender, RoutedEventArgs e)
    {
        MessageDialog dlg = new("Select One, Two, Or Three", "Sample");
        
        try
        {
            IntPtr hwnd = WindowNative.GetWindowHandle(this);
            InitializeWithWindow.Initialize(dlg, hwnd);

            dlg.Commands.Add(new UICommand("One", null, 1));
            dlg.Commands.Add(new UICommand("Two", null, 2));
            dlg.Commands.Add(new UICommand("Three", null, 3));

            IUICommand command = await dlg.ShowAsync();

            text1.Text = $"Command {command.Id} with the label {command.Label} invoked";
        }
        catch (Exception ex)
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
}
