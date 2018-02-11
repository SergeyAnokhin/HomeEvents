using System.Collections.Generic;
using MachineLearningModule.Events;

namespace MachineLearningModule.Brain.Model
{
    public class ClassificationInputData
    {
        /// <summary>
        /// Class name filled if it sample data. empty if data for prediction
        /// </summary>
        public string ClassName { get; set; }
        public IEnumerable<HomeEvent> Events { get; set; }

        public ClassificationInputData(IEnumerable<HomeEvent> events)
        {
            this.Events = events;
        }

        public ClassificationInputData(IEnumerable<HomeEvent> events, string className)
        {
            this.Events = events;
            ClassName = className;
        }
    }
}