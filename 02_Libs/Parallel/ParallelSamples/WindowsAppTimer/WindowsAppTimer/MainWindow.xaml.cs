using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.ComponentModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WindowsAppTimer
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window, INotifyPropertyChanged
    {
        private DispatcherTimer _timer = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindow()
        {
            this.Title = "WinUI Dispatcher Timer App";
            this.InitializeComponent();
            _timer.Tick += OnTick;
            _timer.Interval = TimeSpan.FromSeconds(1);
        }

        private void OnTimer() => _timer.Start();

        private double _timerAngle;
        public double TimerAngle
        {
            get => _timerAngle;
            set
            {
                if (!EqualityComparer<double>.Default.Equals(_timerAngle, value))
                {
                    _timerAngle = value;
                    PropertyChanged?.Invoke(this, new(nameof(TimerAngle)));
                }
            }
        }

        private void OnTick(object? sender, object e)
        {
            double newAngle = TimerAngle + 6;
            if (newAngle >= 360) newAngle = 0;
            TimerAngle = newAngle;
        }

        private void OnStopTimer() => _timer.Stop();
    }
}
