using EventViews.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

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
                new("GP Azerbaijan, Baku", new DateTime(2021, 6, 6)),
                new("GP Canada, Montreal", new DateTime(2021, 6, 13)),
                new("GP France, Le Castellet", new DateTime(2021, 6, 27)),
                new("GP Austria, Spielberg", new DateTime(2021, 7, 4)),
                new("GP Great Britain, Silverstone", new DateTime(2021, 7, 18)),
                new("GP Hungary, Budapest", new DateTime(2021, 8, 1)),
                new("GP Belgium, Spa-Francorchamps", new DateTime(2021, 8, 29)),
                new("GP Netherlands, Zandvoort", new DateTime(2021, 9, 5)),
                new("GP Italy, Monza", new DateTime(2021, 9, 12)),
                new("GP Russia, Sotschi", new DateTime(2021, 9, 26)),
                new("GP Singapore", new DateTime(2021, 10, 3)),
                new("GP Japan, Suzuka", new DateTime(2021, 10, 10)),
                new("GP United States, Austin", new DateTime(2021, 10, 24)),
                new("GP Mexico, Mexico-City", new DateTime(2021, 10, 31)),
                new("GP Brazil, Sao Paulo", new DateTime(2021, 11, 7)),
                new("GP Australia, Melbourne", new DateTime(2021, 11, 21)),
                new("GP Saudi Arabia, Dschidda", new DateTime(2021, 12, 5)),
                new("GP Abu Dhabi", new DateTime(2021, 12, 12))
            };
    }
}
