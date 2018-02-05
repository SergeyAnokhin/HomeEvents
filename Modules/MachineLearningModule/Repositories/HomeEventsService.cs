﻿using System;
using System.Collections.Generic;
using Common;
using Interfaces;
using Nest;

namespace MachineLearningModule.Repositories
{
    public class HomeEventsService : IHomeEventsService
    {
        private readonly Config.Config config;
        private readonly IElasticSearchService elastic;

        public HomeEventsService(IAppConfigService config, IElasticSearchService elastic)
        {
            this.config = config.GetModuleConfig<Config.Config>();
            this.elastic = elastic;
        }

        public IEnumerable<ElasticSearchEvent> GetEventsWindow(DateTime endDateTime)
        {
            var queryForm = new TermQuery
            {
                Field = "doc",
                Value = "event"
            };

            var rangeQuery = new DateRangeQuery
            {
                Field = "@timestamp",
                LessThanOrEqualTo = endDateTime,
                GreaterThanOrEqualTo = endDateTime.AddSeconds(-config.EventsWindowsSeconds)
            };

            var searchRequest = new SearchRequest {Query = queryForm && rangeQuery};

            return elastic.Request<ElasticSearchEvent>(searchRequest);
        }
    }
}
