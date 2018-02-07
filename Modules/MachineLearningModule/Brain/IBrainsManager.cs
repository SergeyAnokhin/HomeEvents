using System.Collections.Generic;
using MachineLearningModule.Events;

namespace MachineLearningModule.Brain
{
    public interface IBrainsManager
    {
        IEnumerable<BrainInfo> GetActiveBrains();
        IEnumerable<BrainPrediction> Predict(List<HomeEvent> events);
        IEnumerable<BrainPrediction> AddToModel(IEnumerable<HomeEvent> events, string className);
    }
}