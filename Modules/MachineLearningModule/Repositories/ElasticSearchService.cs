using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Config;
using Nest;

namespace MachineLearningModule.Repositories
{
    public class ElasticSearchService : IElasticSearchService
    {
        private readonly ILogService log;
        private readonly Config.Config config;

        public ElasticSearchService(IAppConfigService configService, ILogService log)
        {
            this.log = log.Init(GetType());
            config = configService.GetModuleConfig<Config.Config>();
        }

        public IEnumerable<T> Request<T>(SearchRequest searchRequest) where T : class
        {
            searchRequest.Size = searchRequest.Size ?? 500;

            var settings = new ConnectionSettings(
                new Uri(config.ElasticSearchService.Hosts.Last())
            );

            log.Info($"Connect to elastic search : {config.ElasticSearchService.Hosts.Last()}");
            if (config.ElasticSearchService.IsEnableDebuggingRequestResponse)
            {
                settings.DisableDirectStreaming()
                    .PrettyJson();
            }

            var client = new ElasticClient(settings);
            
            var response = client.Search<T>(searchRequest);
            if (response.ServerError != null)
            {
                throw new Exception(response.ServerError.Error.Reason);
            }
            log.Info(response.DebugInformation);

            if (typeof(T).GetInterfaces().Contains(typeof(IHasId)))
            {
                return response.Hits.Select(hit =>
                {
                    var run = hit.Source;
                    (run as IHasId).Id = hit.Id;
                    return run;
                });
            }

            return response.Documents;
        }
    }
}
