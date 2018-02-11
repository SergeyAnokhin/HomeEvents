using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            this.log = log.Init(GetType(), "ElasticSearch");
            config = configService.GetModuleConfig<Config.Config>();
        }

        public IEnumerable<T> Request<T>(SearchRequest searchRequest) where T : class
        {
            var client = Connect();
            searchRequest.Size = searchRequest.Size ?? 500;

            var response = client.Search<T>(searchRequest);
            return PostProcessing(response);
        }

        private IEnumerable<T> PostProcessing<T>(ISearchResponse<T> response) where T : class
        {
            if (response.ServerError != null)
            {
                throw new Exception(response.ServerError.Error.Reason);
            }
            log.Info(FormatDebugInformation(response.DebugInformation));

            if (typeof(T).GetInterfaces().Contains(typeof(IHasId)))
            {
                {
                    return response.Hits.Select(hit =>
                    {
                        var run = hit.Source;
                        (run as IHasId).Id = hit.Id;
                        return run;
                    });
                }
            }
            return response.Documents;
        }

        private string FormatDebugInformation(string info)
        {
            info = info.Replace("# Request:", "<br># <b>Request</b>:<br>");
            var infos = Regex.Split(info, "# Response:");
            var response = infos[1].Crop(500);
            var request = infos[0].UrlToAFref();
            return $"{request}<br># <b>Response</b>:<br>{response} ...";
        }

        public IEnumerable<T> Request<T>(Func<SearchDescriptor<T>, ISearchRequest> func) where T : class
        {
            var client = Connect();
            // searchRequest.Size = searchRequest.Size ?? 500;

            var response = client.Search(func);
            return PostProcessing(response);
        }

        private ElasticClient Connect()
        {
            var settings = new ConnectionSettings(
                        new Uri(config.ElasticSearchService.Hosts.Last())
                    )
                    .DefaultIndex("history-*")
                ;

            var url = config.ElasticSearchService.Hosts.Last();
            log.Info($"Connect to elastic search : <a href='{url}'>{url}</a>");
            if (config.ElasticSearchService.IsEnableDebuggingRequestResponse)
            {
                settings.DisableDirectStreaming()
                    .PrettyJson();
            }

            var client = new ElasticClient(settings);
            return client;
        }
    }
}
