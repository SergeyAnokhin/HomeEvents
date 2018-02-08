using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Config;
using MachineLearningModule.Config;
using MachineLearningModule.Events;
using Newtonsoft.Json;

namespace MachineLearningModule.Brain.Services
{
    class SkLearnBrainApiAdapter : IBrainApiAdapter
    {
        private readonly IApiService api;
        private SkLearnBrainApiAdapterConfig config;

        public SkLearnBrainApiAdapter(IApiService api, IAppConfigService configService)
        {
            this.api = api;
            var moduleConfig = configService.GetModuleConfig<Config.Config>();
            config = moduleConfig.SkLearnBrainApiAdapterConfig;
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
            var parameter = ToAddToModelParameter(events, className);
            var postData = JsonConvert.SerializeObject(parameter);
            string responseData = api.Request("add_to_model", postData);
            return JsonConvert.DeserializeObject<BrainPrediction>(responseData);
        }

        public AddToModelApiParameter ToAddToModelParameter(IEnumerable<HomeEvent> events, string className)
        {
            events = events.ToList();
            return new AddToModelApiParameter
            {
                EventsOrderInImage = config.EventsOrder,
                Image = GetImage(events),
                HomeEvents = events,
                ClassName = className
            };
        }

        private int[] GetImage(IEnumerable<HomeEvent> events)
        {
            var orderedEvents = events
                .Where(e => config.EventsOrder.Contains(e.TypeId))
                .OrderByDescending(e => e.DateTime).ToList();

            var first = orderedEvents.First();
            var rows = orderedEvents
                .GroupBy(e => GetImageStep(e, first))
                .Select(g => CreateImageRow(g.ToList()));

            return rows.SelectMany(r => r).ToArray();
        }

        private List<int> CreateImageRow(List<HomeEvent> homeEvents)
        {
            return config.EventsOrder
                .Select(t => homeEvents.Count(e => e.TypeId == t))
                .ToList();
        }

        private int GetImageStep(HomeEvent homeEvent, HomeEvent first)
        {
            var stepSize = config.ImageStepSeconds;
            var seconds = (homeEvent.DateTime - first.DateTime).TotalSeconds;
            var stepNumber = (int)Math.Floor(seconds / stepSize);
            return stepNumber;
        }
    }

    class AddToModelApiParameter
    {
        public IEnumerable<HomeEvent> HomeEvents { get; set; }
        public int[] Image { get; set; }
        public string[] EventsOrderInImage { get; set; }
        public string ClassName { get; set; }
    }
}
