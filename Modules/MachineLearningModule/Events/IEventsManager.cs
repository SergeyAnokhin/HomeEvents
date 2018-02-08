using System;
using System.Collections.Generic;
using Common;

namespace MachineLearningModule.Events
{
    public interface IEventsManager : IService
    {
        IEnumerable<HomeEvent> GetEventsForSelect(string id);
        IEnumerable<HomeEvent> GetEventsForSelect(DateTime dateTime);
        void SendToBrain(List<string> ids, string className);
        void BrainPredict(List<string> ids);
    }
}
