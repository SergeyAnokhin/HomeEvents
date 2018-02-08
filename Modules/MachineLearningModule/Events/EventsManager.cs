using System;
using System.Collections.Generic;
using System.Linq;
using MachineLearningModule.Brain;
using MachineLearningModule.Repositories;

namespace MachineLearningModule.Events
{
    public class EventsManager : IEventsManager
    {
        private readonly IHomeEventsService homeEvents;
        private readonly IBrainsManager brainsManager;

        public EventsManager(IHomeEventsService homeEvents, IBrainsManager brainsManager)
        {
            this.homeEvents = homeEvents;
            this.brainsManager = brainsManager;
        }

        public IEnumerable<HomeEvent> GetEventsForSelect(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HomeEvent> GetEventsForSelect(DateTime dateTime)
        {
            return homeEvents.GetEventsWindow(dateTime);
        }

        public void SendToBrain(List<string> ids, string className)
        {
            var events = homeEvents.GetEvents(ids);
            brainsManager.AddToModel(events, className);
        }

        public void BrainPredict(List<string> ids)
        {
            var events = homeEvents.GetEvents(ids).ToList();
            brainsManager.Predict(events);
        }
    }
}