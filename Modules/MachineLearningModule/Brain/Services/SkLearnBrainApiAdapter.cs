using System.Collections.Generic;
using Common;
using MachineLearningModule.Events;
using Newtonsoft.Json;

namespace MachineLearningModule.Brain.Services
{
    class SkLearnBrainApiAdapter : IBrainApiAdapter
    {
        private readonly IApiService api;

        public SkLearnBrainApiAdapter(IApiService api)
        {
            this.api = api;
        }

        public bool IsActive()
        {
            return api.Ping();
        }

        public BrainInfo GetBrainInfo()
        {
            return new BrainInfo
            {
                Api = "http://127.0.0.1:0000", // TODO
                Name = "Sklearn",
                SelfScore = float.NaN // TODO
            };
        }

        public BrainPrediction Predict(IEnumerable<HomeEvent> events)
        {
            var postData = JsonConvert.SerializeObject(events);
            string responseData = api.Request("predict", postData);
            return JsonConvert.DeserializeObject<BrainPrediction>(responseData);
        }

        public BrainPrediction AddToModel(IEnumerable<HomeEvent> events, string className)
        {
            var postData = JsonConvert.SerializeObject(new AddToModelApiParameter
            {
                HomeEvents = events,
                ClassName = className
            });
            string responseData = api.Request("add_to_model", postData);
            return JsonConvert.DeserializeObject<BrainPrediction>(responseData);
        }
    }

    class AddToModelApiParameter
    {
        public IEnumerable<HomeEvent> HomeEvents { get; set; }
        public string ClassName { get; set; }
    }
}
