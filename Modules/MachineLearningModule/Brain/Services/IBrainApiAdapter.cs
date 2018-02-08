using System.Collections.Generic;
using MachineLearningModule.Events;

namespace MachineLearningModule.Brain.Services
{
    public interface IBrainApiAdapter
    {
        bool IsActive();
        BrainInfo GetBrainInfo();
        BrainPrediction Predict(IEnumerable<HomeEvent> events);
        BrainPrediction AddToModel(IEnumerable<HomeEvent> events, string className);
    }
}