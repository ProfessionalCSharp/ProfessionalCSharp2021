using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsSample
{
    [EventSource(Name = "Wrox.ProCSharp.MetricsSample")]
    public class MainRunnerSource : EventSource
    {
        private readonly EventCounter _requestCounter;
        private readonly EventCounter _requestTimeCounter;
        public MainRunnerSource()
        {
            _requestCounter = new EventCounter("request-counts", this)
            {
                DisplayName = "Number of requests",
                DisplayUnits = "number"
            };
            _requestTimeCounter = new EventCounter("request-time", this)
            {
                DisplayName = "Request processing time",
                DisplayUnits = "ms"
            };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _requestCounter.Dispose();
                _requestTimeCounter.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
