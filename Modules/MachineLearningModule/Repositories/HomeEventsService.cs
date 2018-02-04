using Common;
using Interfaces;

namespace MachineLearningModule.Repositories
{
    public class HomeEventsService : IHomeEventsService
    {
        private readonly IAppConfigService config;
        private readonly IElasticSearchService elastic;

        public HomeEventsService(IAppConfigService config, IElasticSearchService elastic)
        {
            this.config = config;
            this.elastic = elastic;
        }
    }
}
