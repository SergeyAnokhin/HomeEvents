using System;
using System.Collections.Generic;
using Common;

namespace MachineLearningModule.Repositories
{
    public interface IHomeEventsService : IService
    {
        IEnumerable<ElasticSearchEvent> GetEventsWindow(DateTime dateTime);
    }
}
