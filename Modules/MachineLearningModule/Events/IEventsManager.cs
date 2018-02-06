using System;
using System.Collections.Generic;

namespace MachineLearningModule.Events
{
    public interface IEventsManager
    {
        IEnumerable<HomeEvent> GetEventsForSelect(string id);
        IEnumerable<HomeEvent> GetEventsForSelect(DateTime dateTime);
        bool SendToBrain(List<string> ids, string className);
    }
}
