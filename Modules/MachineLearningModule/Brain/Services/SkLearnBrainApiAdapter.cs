﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Common;
using Common.Config;
using MachineLearningModule.Brain.Model;
using MachineLearningModule.Config;
using MachineLearningModule.Events;
using Newtonsoft.Json;

namespace MachineLearningModule.Brain.Services
{
    public class SkLearnBrainApiAdapter : ABrainApiAdapter
    {
        private SkLearnBrainApiAdapterConfig config => Config.SkLearnBrainApiAdapterConfig;

        public SkLearnBrainApiAdapter(IApiService api, IAppConfigService configService) : base(api, configService)
        {
        }

        public override bool IsActive()
        {
            return api.Ping();
        }

        public override BrainInfo GetBrainInfo()
        {
            return new BrainInfo
            {
                Api = api.EntryPoint,
                Name = "Sklearn",
                SelfScore = float.NaN // TODO
            };
        }

        public override IEnumerable<ClassificationPrediction> Predict(IEnumerable<ClassificationInputData> data)
        {
            var parameter = data.Select(d => ConvertToModelImage(d.Events));
            var postData = JsonConvert.SerializeObject(parameter);
            string responseData = api.Request("predict", postData);
            return JsonConvert.DeserializeObject<List<ClassificationPrediction>>(responseData);
        }

        public override IEnumerable<ClassificationPrediction> Fit(IEnumerable<ClassificationInputData> data)
        {
            var parameter = data.Select(d => ConvertToModelImage(d.Events, d.ClassName));
            var postData = JsonConvert.SerializeObject(parameter);
            string responseData = api.Request("fit", postData);
            return JsonConvert.DeserializeObject<List<ClassificationPrediction>>(responseData);
        }

        public ModelImage ConvertToModelImage(IEnumerable<HomeEvent> events, string className = null)
        {
            events = events.ToList();
            return new ModelImage
            {
                EventsOrderInImage = config.EventsOrder,
                Image = GetImage(events),
                // HomeEvents = events,
                ClassName = className
            };
        }

        private int[] GetImage(IEnumerable<HomeEvent> events)
        {
            var orderedEvents = events
                .Where(e => config.EventsOrder.Contains(e.TypeId))
                .GroupBy(e => e.TypeId + e.DateTime.ToString(CultureInfo.InvariantCulture)) // ignore exactly same event 
                .Select(e => e.First())
                .OrderByDescending(e => e.DateTime).ToList();

            var first = orderedEvents.First();
            var rows = orderedEvents
                .GroupBy(e => GetImageStep(e, first))
                .ToDictionary(k => k.Key, g => CreateImageRow(g.ToList()));

            var empty = CreateEmptyImage(config.TotalImageSteps, config.EventsOrder.Length);

            var image = empty
                .Select(p => GetFromDictionaryIfExist(p.Key, p.Value, rows))
                .ToList();

            return image.SelectMany(r => r).ToArray();
        }

        private IEnumerable<int> GetFromDictionaryIfExist(int step, IEnumerable<int> emptyRow, Dictionary<int, List<int>> dict)
        {
            if (dict.ContainsKey(step)) return dict[step];
            return emptyRow.ToList();
        }

        private Dictionary<int, IEnumerable<int>> CreateEmptyImage(int configTotalImageSteps, int eventsOrderLength)
        {
            return Enumerable.Range(0, configTotalImageSteps)
                .ToDictionary(k => k, v => Enumerable.Repeat(0, eventsOrderLength));
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
            var seconds = (first.DateTime - homeEvent.DateTime).TotalSeconds;
            var stepNumber = (int)Math.Floor(seconds / stepSize);
            return stepNumber;
        }
    }

    public class ModelImage
    {
        public IEnumerable<HomeEvent> HomeEvents { get; set; }
        public int[] Image { get; set; }
        public string[] EventsOrderInImage { get; set; }
        public string ClassName { get; set; }

        public override string ToString()
        {
            return
                $"<u>{(ClassName ?? "(undefined)")}</u> => {Environment.NewLine}" +
                Image.Split(EventsOrderInImage.Length)
                    .Select(r => r.StringJoin())
                    .StringJoin(Environment.NewLine);
        }
    }
}
