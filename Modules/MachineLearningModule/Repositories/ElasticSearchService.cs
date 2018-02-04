using Common;
using Interfaces;

namespace MachineLearningModule.Repositories
{
    public class ElasticSearchService : IElasticSearchService, IEventRepositoryService
    {
        private readonly IAppConfigService config;

        public ElasticSearchService(IAppConfigService config)
        {
            this.config = config;
        }
    }
}
