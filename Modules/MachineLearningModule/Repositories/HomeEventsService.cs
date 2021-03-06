﻿using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Config;
using MachineLearningModule.Events;
using Nest;

namespace MachineLearningModule.Repositories
{
    public class HomeEventsService : IHomeEventsService
    {
        private readonly Config.Config config;
        private readonly IElasticSearchService elastic;
        private ILogService log;

        public HomeEventsService(IAppConfigService config, IElasticSearchService elastic, ILogService log)
        {
            this.log = log.Init(GetType());
            this.config = config.GetModuleConfig<Config.Config>();
            this.elastic = elastic;
        }

        public IEnumerable<HomeEvent> GetEventsWindow(DateTime endDateTime)
        {
            var match = new MatchPhraseQuery
            {
                Field = new Field("doc"),
                Query = "event"
            };

            var rangeQuery = new DateRangeQuery
            {
                Field = "@timestamp",
                LessThanOrEqualTo = endDateTime.ToUniversalTime(),
                GreaterThanOrEqualTo = endDateTime.ToUniversalTime().AddSeconds(-config.EventsWindowsSeconds),
            };

            var boolQuery = new BoolQuery
            {
                Must = new List<QueryContainer>
                {
                    match,
                    rangeQuery
                }
            };

            var searchRequest = new SearchRequest(Indices.Parse("history-*"))
            {
                Query = boolQuery,
                Sort = new List<ISort>()
                {
                    new SortField
                    {
                        Field = new Field("@timestamp"),
                        Order = SortOrder.Descending
                    }
                }
            };

            var result = elastic
                .Request<ElasticSearchEvent>(searchRequest)
                .Select(ConvertToLocalDateTime)
                .ToList();
            result.ForEach(r => log.Debug(r.ToString()));

            return result.Select(r => new HomeEvent
            {
                DateTime = r.timestamp,
                Id = r.Id,
                Sensor = r.sensor.display,
                Status = r.status,
                SensorType = r.sensor.type,
            });
        }

        private static ElasticSearchEvent ConvertToLocalDateTime(ElasticSearchEvent r)
        {
            r.timestamp = TimeZoneInfo.ConvertTimeFromUtc(r.timestamp, TimeZoneInfo.Local);
            return r;
        }

        public IEnumerable<HomeEvent> GetEvents(List<string> ids)
        {
            Func<SearchDescriptor<ElasticSearchEvent>, ISearchRequest> selector = (d) =>
                d.Index("history-*")
                    .Size(500)
                    .Query(q => q.Ids(c =>
                        c.Values(ids).Name("Get events by IDs : " + ids.ToLog())));

            var result = elastic
                .Request(selector)
                .Select(ConvertToLocalDateTime);
            return result.Select(r => new HomeEvent
            {
                DateTime = r.timestamp,
                Id = r.Id,
                Sensor = r.sensor.display,
                Status = r.status,
                SensorType = r.sensor.type,
            });

        }
    }
}
