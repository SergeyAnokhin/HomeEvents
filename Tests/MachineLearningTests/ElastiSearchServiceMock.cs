﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;
using MachineLearningModule.Repositories;
using Nest;
using Newtonsoft.Json;

namespace MachineLearningTests
{
    class ElastiSearchServiceMock : IElasticSearchService
    {
        private readonly Stack<string> responseDataFiles;
        public ElastiSearchServiceMock(IEnumerable<string> responseDataFiles)
        {
            this.responseDataFiles = new Stack<string>(responseDataFiles);
        }

        public IEnumerable<T> Request<T>(SearchRequest searchRequest) where T : class
        {
            var file = responseDataFiles.Peek();
            var body = File.ReadAllText(file);
            var result = JsonConvert.DeserializeObject<ElasticSearchResponse<T>>(body);
            return result.hits.hits.Select(h =>
            {
                var id = h._source as IHasId;
                if (id != null)
                {
                    id.Id = h._id;
                }
                return h._source;
            });
        }
    }

    class ElasticSearchResponse<T>
    {
        public ElasticSearchResponseItems<T> hits { get; set; }
    }

    class ElasticSearchResponseItems<T>
    {
        public ElasticSearchResponseHit<T>[] hits { get; set; }
    }

    class ElasticSearchResponseHit<T>
    {
        public string _id { get; set; }
        public T _source { get; set; }
    }

}