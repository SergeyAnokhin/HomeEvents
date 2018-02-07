using System;
using System.Collections.Generic;
using Common;

namespace MachineLearningModule.Events
{
    public interface IEventsManager : IService
    {
        IEnumerable<HomeEvent> GetEventsForSelect(string id);
        IEnumerable<HomeEvent> GetEventsForSelect(DateTime dateTime);
        bool SendToBrain(List<string> ids, string className);
    }
}
