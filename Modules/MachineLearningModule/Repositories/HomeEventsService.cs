using System;
using System.Collections.Generic;
using Common;
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

        public IEnumerable<ElasticSearchEvent> GetEventsWindow(DateTime endDateTime)
        {
            var match = new MatchPhraseQuery
            {
                Field = new Field("doc"),
                Query = "event"
            };

            var rangeQuery = new DateRangeQuery
            {
                Field = "@timestamp",
                LessThanOrEqualTo = endDateTime,
                GreaterThanOrEqualTo = endDateTime.AddSeconds(-config.EventsWindowsSeconds)
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

            var result = elastic.Request<ElasticSearchEvent>(searchRequest);
            return result;
        }
    }
}
