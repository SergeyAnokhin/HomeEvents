using System;
using System.Collections.Generic;
using Common;
using Nest;

namespace MachineLearningModule.Repositories
{
    public interface IElasticSearchService : IService
    {
        IEnumerable<T> Request<T>(SearchRequest searchRequest) where T : class;
        IEnumerable<T> Request<T>(Func<SearchDescriptor<T>, ISearchRequest> func) where T : class;
    }
}
