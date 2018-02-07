using System;
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
                // TimeZone = "+01:00"
            };

            var boolQuery = new BoolQuery
            {
                Must = new List<QueryContainer>
                {
                    match,
                    rangeQuery
                }
            };

            var searchRequest = new SearchRequest
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

            var result = elastic.Request<ElasticSearchEvent>(searchRequest).ToList();
            result.ForEach(r => log.Debug(r.ToString()));

            return result.Select(r => new HomeEvent
            {
                DateTime = TimeZoneInfo.ConvertTimeFromUtc(r.timestamp, TimeZoneInfo.Local),
                Id = r.Id,
                Sensor = r.sensor.display,
                Status = r.status,
                SensorType = r.sensor.type,
            });
        }

        public List<HomeEvent> GetEvents(List<string> ids)
        {
            throw new NotImplementedException();
        }
    }
}
