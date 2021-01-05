using Microsoft.UI.Xaml;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WindowsAppTimer
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private DispatcherTimer _timer = new();

        public MainWindow()
        {
            this.InitializeComponent();
            _timer.Tick += OnTick;
            _timer.Interval = TimeSpan.FromSeconds(1);
        }

        private void OnTimer()
        {
            _timer.Start();
        }

        private void OnTick(object sender, object e)
        {
            double newAngle = rotate.Angle + 6;
            if (newAngle >= 360) newAngle = 0;
            rotate.Angle = newAngle;
        }

        private void OnStopTimer()
        {
            _timer.Stop();
        }
    }
}
