using Interfaces;

namespace MachineLearningModule.Repositories
{
    public class HomeEventsService : IHomeEventsService
    {
        private readonly IElasticSearchService elasticSearch;

        public HomeEventsService(IElasticSearchService elasticSearch)
        {
            this.elasticSearch = elasticSearch;
        }
    }
}
