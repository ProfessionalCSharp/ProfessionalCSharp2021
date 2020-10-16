using System;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Threading;

namespace MetricsSample
{
    [EventSource(Name = "Wrox.ProCSharp.MetricsSample")]
    internal class MainRunnerSource : EventSource
    {
        public static readonly MainRunnerSource Log = new MainRunnerSource();

        private PollingCounter? _totalRequestsCounter;
        private PollingCounter? _currentRequestsCounter;
        private EventCounter? _requestDurationCounter;
        private long _totalRequests;
        private long _currentRequests;

        private MainRunnerSource()
            : base("Wrox.ProCSharp.MetricsSample")
        {

        }

        [Event(1, Level = EventLevel.Informational)]
        public Stopwatch? RequestStart(string method, string path)
        {
            Interlocked.Increment(ref _totalRequests);
            Interlocked.Increment(ref _currentRequests);
            WriteEvent(1, method, path);
            if (IsEnabled())
            {
                return Stopwatch.StartNew();
            }
            else
            {
                return default;
            }
        }

        [Event(2, Level = EventLevel.Informational)]
        public void RequestStop(Stopwatch? stopwatch)
        {
            Interlocked.Decrement(ref _currentRequests);
            WriteEvent(2);
            if (stopwatch?.IsRunning == true)
            {
                stopwatch.Stop();
                _requestDurationCounter?.WriteMetric(stopwatch.ElapsedMilliseconds);
            }
        }

        [Event(3, Level = EventLevel.Error)]
        public void Error(Exception ex)
        {
            WriteEvent(3, ex.Message);
        }


        protected override void OnEventCommand(EventCommandEventArgs command)
        {
            if (command.Command == EventCommand.Enable)
            {
                _totalRequestsCounter ??= new PollingCounter("metricssample-total-requests", this, () => Volatile.Read(ref _totalRequests))
                {
                    DisplayName = "Total Requests"
                };
                _currentRequestsCounter ??= new PollingCounter("metricssample-current-requests", this, () => Volatile.Read(ref _currentRequests))
                {
                    DisplayName = "Current Requests"
                };
                _requestDurationCounter ??= new EventCounter("metricssample-request-duration", this)
                {
                    DisplayName = "Request duration",
                    DisplayUnits = "ms"
                };
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _requestDurationCounter?.Dispose();
                _requestDurationCounter = null;
                _totalRequestsCounter?.Dispose();
                _totalRequestsCounter = null;
                _currentRequestsCounter?.Dispose();
                _currentRequestsCounter = null;
            }
            base.Dispose(disposing);
        }
    }
}
