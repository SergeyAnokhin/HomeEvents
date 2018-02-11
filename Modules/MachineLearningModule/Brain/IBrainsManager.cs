using System.Collections.Generic;
using Common;
using MachineLearningModule.Brain.Model;
using MachineLearningModule.Events;

namespace MachineLearningModule.Brain
{
    public interface IBrainsManager : IService
    {
        IEnumerable<BrainInfo> GetActiveBrains();
        IEnumerable<ClassificationPrediction> Predict(List<HomeEvent> events);
        IEnumerable<ClassificationPrediction> AddToModel(IEnumerable<HomeEvent> events, string className);
    }
}