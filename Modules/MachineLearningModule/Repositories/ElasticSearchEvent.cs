using System;
using Common;
using Nest;
using Newtonsoft.Json;

namespace MachineLearningModule.Repositories
{
    [ElasticsearchType(IdProperty = "Id", Name= "measure")]
    public class ElasticSearchEvent : IHasId
    {
        public string Id { get; set; }
        [Date(Name = "@timestamp")]
        [JsonProperty("@timestamp")]
        public DateTime timestamp { get; set; }
        public ElasticSearchEventSensor sensor { get; set; }
        public float value { get; set; }
        public string status { get; set; }

        public override string ToString()
        {
            return $"[{this.GetType().Name}: @{timestamp:HH:mm:ss} {sensor} : '{status}' ({value}) #ID: {Id}]";
        }
    }

    public class ElasticSearchEventSensor
    {
        public string display { get; set; }
        public string type { get; set; }

        public override string ToString()
        {
            return $"[{type}]{display}";
        }
    }
}
