using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;

namespace WindowsApp;

public sealed partial class MainWindow : Window
{
    private readonly Calculator _calculator = new();

    private void ShowInfo(string message, bool clear = false)
    {
        void AddMessage(string message, bool clear)
        {
            if (clear)
            {
                listStatus.Items.Clear();
            }
            listStatus.Items.Add(message);
        }

        if (!DispatcherQueue.HasThreadAccess)
        {
            DispatcherQueue.TryEnqueue(() =>
            {
                AddMessage(message, clear);
            });
        }
        else
        {
            AddMessage(message, clear);
        }
    }

    public void ShowThreadAndTaskInfo(string title, bool clear = false)
    {
        string taskMessage = Task.CurrentId == null ? "no task" : $"in the task {Task.CurrentId}";

        ShowInfo($"{title}{Environment.NewLine}Running in thread {Environment.CurrentManagedThreadId} and {taskMessage}{Environment.NewLine}", clear);
    }

    public MainWindow() => InitializeComponent();

    private void BlockingAdd(object sender, RoutedEventArgs e)
    {
        ShowThreadAndTaskInfo("BlockingAdd started", true);
        if (!int.TryParse(text1.Text, out int x))
        {
            ShowInfo("enter a valid number");
            return;
        }
        if (!int.TryParse(text2.Text, out int y))
        {
            ShowInfo("enter a valid number");
            return;
        }
        int result = _calculator.BlockingAdd(x, y);
        textResult.Text = result.ToString();
        ShowThreadAndTaskInfo("Completed");
    }

    private void CustomTask(object sender, RoutedEventArgs e)
    {
        ShowThreadAndTaskInfo("CustomTask started", true);
        if (!int.TryParse(text1.Text, out int x))
        {
            ShowInfo("enter a valid number");
            return;
        }
        if (!int.TryParse(text2.Text, out int y))
        {
            ShowInfo("enter a valid number");
            return;
        }
        Task.Run(() =>
        {
            ShowThreadAndTaskInfo("task in CustomTask started");
            int result = _calculator.BlockingAdd(x, y);
            // textResult.Text = result.ToString(); // wrong thread
            DispatcherQueue.TryEnqueue(() =>
            {
                textResult.Text = result.ToString();
                ShowThreadAndTaskInfo("Completed");
            });
        });
    }

    private async void InvokeCalculator(object sender, RoutedEventArgs e)
    {
        ShowThreadAndTaskInfo("InvokeCalculator started", true);

        if (!int.TryParse(text1.Text, out int x))
        {
            ShowInfo("enter a valid number");
            return;
        }
        if (!int.TryParse(text2.Text, out int y))
        {
            ShowInfo("enter a valid number");
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
        ShowThreadAndTaskInfo("After async method");
        textResult.Text = result.ToString();
    }

    private async void NotUsingCapturedContext(object sender, RoutedEventArgs e)
    {
        try
        {
            var s = SynchronizationContext.Current;
            ShowThreadAndTaskInfo("NotUsingCapturedContext started", true);
            if (!int.TryParse(text1.Text, out int x))
            {
                ShowInfo("enter a valid number");
                return;
            }
            if (!int.TryParse(text2.Text, out int y))
            {
                ShowInfo("enter a valid number");
                return;
            }
            int result = 0;
            result = await _calculator.AddAsync(x, y).ConfigureAwait(continueOnCapturedContext: false);
            ShowThreadAndTaskInfo("After async method");
            textResult.Text = result.ToString();
        }
        catch (Exception ex)
        {
            ShowInfo(ex.Message);
        }
    }

    private async void StartingANewTask(object sender, RoutedEventArgs e)
    {
        try
        {
            await Task.Run(async () =>
            {
                ShowThreadAndTaskInfo("NotUsingCapturedContext started", true);
                if (!int.TryParse(text1.Text, out int x))
                {
                    ShowInfo("enter a valid number");
                    return;
                }
                if (!int.TryParse(text2.Text, out int y))
                {
                    ShowInfo("enter a valid number");
                    return;
                }
                int result = 0;
                result = await _calculator.AddAsync(x, y).ConfigureAwait(continueOnCapturedContext: false);
                ShowThreadAndTaskInfo("After async method");
                textResult.Text = result.ToString();
            });
        }
        catch (Exception ex)
        {
            ShowInfo(ex.Message);
        }
    }
}
