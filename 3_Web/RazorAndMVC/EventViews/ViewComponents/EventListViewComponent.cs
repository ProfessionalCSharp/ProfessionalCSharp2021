using EventViews.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventViews.ViewComponents;

[ViewComponent(Name ="EventList")]
public class EventListViewComponent : ViewComponent
{
    private readonly IEventsService _eventsService;
    public EventListViewComponent(IEventsService eventsService) => _eventsService = eventsService;

    public Task<IViewComponentResult> InvokeAsync(DateTime from, DateTime to) =>
        Task.FromResult<IViewComponentResult>(
            View(EventsByDateRange(from, to)));

    private IEnumerable<Event> EventsByDateRange(DateTime from, DateTime to) =>
        _eventsService.Events.Where(e => e.Date >= from && e.Date <= to);
}
