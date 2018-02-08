using System.Collections.Generic;
using Common;
using MachineLearningModule.Events;

namespace MachineLearningModule.Brain
{
    public interface IBrainsManager : IService
    {
        IEnumerable<BrainInfo> GetActiveBrains();
        IEnumerable<BrainPrediction> Predict(List<HomeEvent> events);
        IEnumerable<BrainPrediction> AddToModel(IEnumerable<HomeEvent> events, string className);
    }
}