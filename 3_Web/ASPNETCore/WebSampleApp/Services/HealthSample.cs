using System;
using System.Threading;

namespace WebSampleApp.Services
{
    public class HealthSample : IDisposable
    {
        private Timer? _timer;
        public void SetHealthy(bool healthy = true)
        {
            if (IsHealthy == healthy) return;

            _isReady = false;
            IsHealthy = healthy;

            if (IsHealthy)
            {
                if (_timer is not null)
                {
                    _timer.Dispose();
                }
                _timer = new(o =>
                {
                    _isReady = true;
                }, null, TimeSpan.FromSeconds(10), Timeout.InfiniteTimeSpan);
            }
        }

        public void Dispose() => _timer?.Dispose();

        public bool IsHealthy { get; set; } = false;

        private bool _isReady = false;
        public bool IsReady => IsHealthy && _isReady;
    }
}
