using System;
using System.Collections.Generic;
using Common;
using MachineLearningModule.Brain;
using MachineLearningModule.Brain.Model;

namespace MachineLearningModule.Events
{
    public interface IEventsManager : IService
    {
        IEnumerable<HomeEvent> GetEventsForSelect(string id);
        IEnumerable<HomeEvent> GetEventsForSelect(DateTime dateTime);
        IEnumerable<ClassificationPrediction> SendToBrain(List<string> ids, string className);
        IEnumerable<ClassificationPrediction> BrainPredict(List<string> ids);
    }
}
