using System;
using System.Collections.Generic;

namespace MachineLearningModule.Events
{
    public class EventsManager : IEventsManager
    {
        public IEnumerable<HomeEvent> GetEventsForSelect(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HomeEvent> GetEventsForSelect(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public bool SendToBrain(List<string> ids, string className)
        {
            throw new NotImplementedException();
        }
    }
}