using System;
using System.Diagnostics;
using System.Diagnostics.Tracing;

namespace LoggingSample
{
    [EventSource(Name = "Wrox.ProCSharp.LoggingSample")]
    internal class LoggingSampleSource : EventSource
    {
        public static readonly LoggingSampleSource Log = new();

        private IncrementingEventCounter? _totalRequestsCounter;
        private IncrementingEventCounter? _requestsPerMinuteCounter;
        private EventCounter? _requestDurationCounter;

        private LoggingSampleSource()
            : base("Wrox.ProCSharp.LoggingSample")
        {
            // https://im5tu.io/article/2020/01/diagnostics-in-.net-core-3-event-counters/
        }

        [Event(1, Level = EventLevel.Informational)]
        public Stopwatch? RequestStart(string method, string path)
        {
            WriteEvent(1, method, path);
            if (IsEnabled())
            {
                _totalRequestsCounter?.Increment();
                _requestsPerMinuteCounter?.Increment();
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
                _totalRequestsCounter ??= new IncrementingEventCounter("loggingsample-total-requests", this)
                {
                    DisplayName = "Total Requests",
                };
                _requestsPerMinuteCounter ??= new IncrementingEventCounter("loggingsample-requests-per-minute", this)
                {
                    DisplayName = "Current Requests",
                    DisplayUnits = "Min",
                    DisplayRateTimeScale = TimeSpan.FromSeconds(60)
                };
                _requestDurationCounter ??= new EventCounter("loggingsample-request-duration", this)
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
                _requestsPerMinuteCounter?.Dispose();
                _requestsPerMinuteCounter = null;
            }
            base.Dispose(disposing);
        }
    }
}
