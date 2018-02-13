using System;
using System.Collections.Generic;
using Common;
using MachineLearningModule.Brain.Model;

namespace MachineLearningModule.Events
{
    public interface IEventsManager : IService
    {
        IEnumerable<HomeEvent> GetEventsForSelect(string id);
        IEnumerable<HomeEvent> GetEventsForSelect(DateTime dateTime);
        IEnumerable<ClassificationPrediction> AddToModel(List<string> ids, string className);
        IEnumerable<ClassificationPrediction> FitModel();
        IEnumerable<ClassificationPrediction> BrainPredict(List<string> ids);
        void AddToModel(Dictionary<DateTime, string> datesForEndEvent);
    }
}
