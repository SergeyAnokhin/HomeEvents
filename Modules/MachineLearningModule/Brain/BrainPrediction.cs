using System.Collections.Generic;

namespace MachineLearningModule.Brain
{
    public class BrainPrediction
    {
        public BrainInfo Brain { get; set; }
        public string Class { get; set; }
        public float Probability { get; set; }
        public Dictionary<string, float> Probabilities { get; set; }
    }
}