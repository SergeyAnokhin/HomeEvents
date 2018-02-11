using System.Collections.Generic;

namespace MachineLearningModule.Brain.Model
{
    public class ClassificationPrediction
    {
        public BrainInfo Brain { get; set; }
        public string Class { get; set; }
        public float Probability { get; set; }
        public Dictionary<string, float> Probabilities { get; set; }
        public int Seconds { get; set; }
    }
}