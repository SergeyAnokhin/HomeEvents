using System;

namespace MachineLearningModule.Events
{
    public class HomeEvent
    {
        public string Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Sensor { get; set; }
        public string Status { get; set; }
        public string SensorType { get; set; }

        public string TypeId => $"[{SensorType}]{Sensor}={Status}";

        public override string ToString()
        {
            return $"[{this.GetType().Name}: @{DateTime:HH:mm:ss} {Sensor} : '{Status}' #ID: {Id}]";
        }
    }
}