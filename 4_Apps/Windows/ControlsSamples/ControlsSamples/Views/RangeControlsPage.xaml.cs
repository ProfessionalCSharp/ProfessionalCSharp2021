using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace ControlsSamples.Views;

public sealed partial class RangeControlsPage : Page
{
    public RangeControlsPage()
    {
        InitializeComponent();
        ShowProgress();
    }

    private void ShowProgress()
    {
        DispatcherTimer timer = new();
        timer.Interval = TimeSpan.FromSeconds(1);
        int i = 0;
        timer.Tick += (sender, e) =>
            progressBar1.Value = i++ % 100;
        timer.Start();
    }
}
