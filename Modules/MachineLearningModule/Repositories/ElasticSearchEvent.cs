using System;

namespace MachineLearningModule.Repositories
{
    public class ElasticSearchEvent
    {
        public DateTime timestamp { get; set; }
        public ElasticSearchEventSensor sensor { get; set; }
        public float value { get; set; }
        public string status { get; set; }
    }

    public class ElasticSearchEventSensor
    {
        public string display { get; set; }
        public string type { get; set; }
    }
}
