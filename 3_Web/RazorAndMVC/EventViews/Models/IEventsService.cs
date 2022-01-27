namespace EventViews.Models;

public interface IEventsService
{
    IEnumerable<Event> Events { get; }
}
