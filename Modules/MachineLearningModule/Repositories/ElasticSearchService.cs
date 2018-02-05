using System;
using System.Collections.Generic;
using Common;
using Interfaces;
using Nest;

namespace MachineLearningModule.Repositories
{
    public class ElasticSearchService : IElasticSearchService
    {
        private readonly Config.Config config;

        public ElasticSearchService(IAppConfigService configService)
        {
            configService.GetModuleConfig<Config.Config>();
        }

        public IEnumerable<T> Request<T>(SearchRequest searchRequest) where T : class
        {
            var node = new Uri(config.ElasticsearchHost);

            var settings = new ConnectionSettings(
                node
                // defaultIndex: "history-*"
            );

            var client = new ElasticClient(settings);

            var response = client.Search<T>(searchRequest);
            return response.Documents;
        }
    }
}
