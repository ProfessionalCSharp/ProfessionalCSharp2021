using Microsoft.UI.Xaml;

using System.Runtime.InteropServices;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WindowsApp;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    private readonly Calculator _calculator = new();

    public MainWindow()
    {
        this.InitializeComponent();
    }

    private async void InvokeCalculator(object sender, RoutedEventArgs e)
    {
        listStatus.Items.Clear();
        listStatus.Items.Add(GetThreadAndTaskInfo("InvokeCalculator started"));
        if (!int.TryParse(text1.Text, out int x))
        {
            listStatus.Items.Add("enter a valid number");
            return;
        }
        if (!int.TryParse(text2.Text, out int y))
        {
            listStatus.Items.Add("enter a valid number");
            return;
        }
        int result = 0;
        if (ReferenceEquals(sender, addButton))
        {
            result = await _calculator.AddAsync(x, y);
        }
        else if (ReferenceEquals(sender, subtractButton))
        {
            result = await _calculator.SubtractAsync(x, y);
        }
        listStatus.Items.Add(GetThreadAndTaskInfo("After async method"));
        textResult.Text = result.ToString();

        var s = SynchronizationContext.Current;
    }

    public static string GetThreadAndTaskInfo(string title)
    {
        string taskMessage = Task.CurrentId == null ? "no task" : $"in the task {Task.CurrentId}";
        return $"{title}{Environment.NewLine}Running in thread {Environment.CurrentManagedThreadId} and {taskMessage}{Environment.NewLine}";
    }

    private async void NotUsingCapturedContext(object sender, RoutedEventArgs e)
    {
        try
        {
            listStatus.Items.Clear();
            listStatus.Items.Add(GetThreadAndTaskInfo("NotUsingCapturedContext started"));
            if (!int.TryParse(text1.Text, out int x))
            {
                listStatus.Items.Add("enter a valid number");
                return;
            }
            if (!int.TryParse(text2.Text, out int y))
            {
                listStatus.Items.Add("enter a valid number");
                return;
            }
            int result = 0;
            result = await _calculator.AddAsync(x, y).ConfigureAwait(continueOnCapturedContext: false);
            listStatus.Items.Add(GetThreadAndTaskInfo("After async method"));
            textResult.Text = result.ToString();
        }
        catch (Exception ex)
        {
            listStatus.Items.Add(ex.Message);
        }
    }

    private void BlockingAdd(object sender, RoutedEventArgs e)
    {
        listStatus.Items.Clear();
        listStatus.Items.Add(GetThreadAndTaskInfo("BlockingAdd started"));
        if (!int.TryParse(text1.Text, out int x))
        {
            listStatus.Items.Add("enter a valid number");
            return;
        }
        if (!int.TryParse(text2.Text, out int y))
        {
            listStatus.Items.Add("enter a valid number");
            return;
        }
        int result = _calculator.BlockingAdd(x, y);
        textResult.Text = result.ToString();
        listStatus.Items.Add(GetThreadAndTaskInfo("Completed"));
    }
}
