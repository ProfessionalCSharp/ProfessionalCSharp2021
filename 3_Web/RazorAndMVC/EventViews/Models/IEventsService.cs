using System.Collections.Generic;

namespace EventViews.Models
{
    public interface IEventsService
    {
        IEnumerable<Event> Events { get; }
    }
}
