using System;
using System.Collections.Generic;
using Common;
using MachineLearningModule.Brain;

namespace MachineLearningModule.Events
{
    public interface IEventsManager : IService
    {
        IEnumerable<HomeEvent> GetEventsForSelect(string id);
        IEnumerable<HomeEvent> GetEventsForSelect(DateTime dateTime);
        IEnumerable<BrainPrediction> SendToBrain(List<string> ids, string className);
        IEnumerable<BrainPrediction> BrainPredict(List<string> ids);
    }
}
