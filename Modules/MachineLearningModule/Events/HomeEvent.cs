using System;

namespace MachineLearningModule.Events
{
    public class HomeEvent
    {
        public DateTime DateTime { get; set; }
        public string Sensor { get; set; }
        public string Status { get; set; }
    }
}