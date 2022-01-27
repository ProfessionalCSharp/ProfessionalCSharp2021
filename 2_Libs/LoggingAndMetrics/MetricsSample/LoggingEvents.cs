using Microsoft.Extensions.Logging;

namespace MetricsSample;

class LoggingEvents
{
    public static EventId Injection { get; } = new EventId(2000, nameof(Injection));
    public static EventId Networking { get; } = new EventId(2002, nameof(Networking));
}
