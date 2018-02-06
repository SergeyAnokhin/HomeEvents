using System;
using System.Collections.Generic;

namespace MachineLearningModule
{
    public interface IEventsManager
    {
        IEnumerable<HomeEvent> GetEventsForSelect(string id);
        IEnumerable<HomeEvent> GetEventsForSelect(DateTime dateTime);
        bool SendToBrain(List<string> ids, string className);
    }

    public class HomeEvent
    {
        public DateTime DateTime { get; set; }
        public string Sensor { get; set; }
        public string Status { get; set; }
    }

    public class EventsManager : IEventsManager
    {
        public IEnumerable<HomeEvent> GetEventsForSelect(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HomeEvent> GetEventsForSelect(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public bool SendToBrain(List<string> ids, string className)
        {
            throw new NotImplementedException();
        }
    }

    public class BrainInfo
    {
        public string Name { get; set; }
        public string Api { get; set; }
        public float SelfScore { get; set; }
    }

    public class BrainPrediction
    {
        public BrainInfo Brain { get; set; }
        public string Class { get; set; }
        public float Probability { get; set; }
        public Dictionary<string, float> Probabilities { get; set; }
    }

    public interface IBrainsManager
    {
        IEnumerable<BrainInfo> GetActiveBrains();
        IEnumerable<BrainPrediction> Predict(List<HomeEvent> events);
    }
}
