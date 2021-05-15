using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ControlsSamples.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RangeControlsPage : Page
    {
        public RangeControlsPage()
        {
            this.InitializeComponent();
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
}
