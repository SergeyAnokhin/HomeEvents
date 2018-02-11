using System.Collections.Generic;
using MachineLearningModule.Brain.Model;

namespace MachineLearningModule.Brain.Services
{
    public interface IBrainApiAdapter
    {
        bool IsActive();
        BrainInfo GetBrainInfo();
        IEnumerable<ClassificationPrediction> Predict(IEnumerable<ClassificationInputData> data);
        ClassificationPrediction Predict(ClassificationInputData data);
        IEnumerable<ClassificationPrediction> Fit(IEnumerable<ClassificationInputData> data);
    }
}