using System;
using System.Collections.Generic;
using MachineLearningModule.Repositories;

namespace MachineLearningModule.Events
{
    public class EventsManager : IEventsManager
    {
        private readonly IHomeEventsService homeEvents;

        public EventsManager(IHomeEventsService homeEvents)
        {
            this.homeEvents = homeEvents;
        }

        public IEnumerable<HomeEvent> GetEventsForSelect(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HomeEvent> GetEventsForSelect(DateTime dateTime)
        {
            return homeEvents.GetEventsWindow(dateTime);
        }

        public bool SendToBrain(List<string> ids, string className)
        {
            throw new NotImplementedException();
        }
    }
}