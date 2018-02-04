using System;
using Common;
using Interfaces;
using Nest;

namespace MachineLearningModule.Repositories
{
    public class ElasticSearchService : IElasticSearchService, IEventRepositoryService
    {
        private readonly Config.Config config;

        public ElasticSearchService(IAppConfigService configService)
        {
            configService.GetModuleConfig<Config.Config>();
        }

        public void Request<T>()
        {
            var node = new Uri(config.ElasticsearchHost);

            var settings = new ConnectionSettings(
                node
                // defaultIndex: "history-*"
            );

            var client = new ElasticClient(settings);

            client.Search<T>(s => s.Query(d => d.DateRange()))
        }
    }
}
