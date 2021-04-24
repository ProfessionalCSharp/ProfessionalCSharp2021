using EventViews.Models;
using System;
using System.Collections.Generic;

namespace WebAppSample.Services
{
    public class Formula1Events : IEventsService
    {
        private List<Event>?_events;
        public IEnumerable<Event> Events => _events ??= GetEvents();

        private List<Event> GetEvents() =>
            new()
            {
                new("GP Bahrain, Sachir", new DateTime(2021, 3, 28)),
                new("GP Emilia Romagna, Imola", new DateTime(2021, 4, 18)),
                new("GP Portugal, Portimao", new DateTime(2021, 5, 2)),
                new("GP Spain, Barcelona", new DateTime(2021, 5, 9)),
                new("GP Monaco, Monte Carlo", new DateTime(2021, 5, 23)),
                new("GP Aserbaidschan, Baku", new DateTime(2021, 6, 6)),
                new("GP Canada, Montreal", new DateTime(2021, 6, 13)),
                new("GP France, Le Castellet", new DateTime(2021, 6, 27)),
                new("GP Austria, Spielberg", new DateTime(2021, 7, 4)),
                new("GP Great Britain, Silverstone", new DateTime(2021, 7, 18)),
                new("GP Hungary, Budapest", new DateTime(2021, 8, 1)),
            };
    }
}
