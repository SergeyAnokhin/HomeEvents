using System;
using System.Collections.Generic;
using System.Linq;
using MachineLearningModule.Brain;
using MachineLearningModule.Brain.Model;
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

        public IEnumerable<ClassificationPrediction> AddToModel(List<string> ids, string className)
        {
            var events = homeEvents.GetEvents(ids);
            return brainsManager.AddToModel(events, className);
        }

        public IEnumerable<ClassificationPrediction> FitModel()
        {
            return brainsManager.FitModel();
        }

        public IEnumerable<ClassificationPrediction> BrainPredict(List<string> ids)
        {
            var events = homeEvents.GetEvents(ids).ToList();
            return brainsManager.Predict(events);
        }

        public IEnumerable<ClassificationPrediction> AddToModel(Dictionary<DateTime, string> datesForEndEvent)
        {
            var data = datesForEndEvent
                .Select(p => new ClassificationInputData(homeEvents.GetEventsWindow(p.Key), p.Value));
            return brainsManager.FitModel(data);
        }
    }
}