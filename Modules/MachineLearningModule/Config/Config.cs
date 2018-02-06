using Common;
using Common.Config;

namespace MachineLearningModule.Config
{
    public class Config
    {
        public int EventsWindowsSeconds { get; set; }
        public int EventsWindowsStep { get; set; }
        public ElasticSearchServiceConfig ElasticSearchService { get; set; }
    }

    public class ElasticSearchServiceConfig
    {
        public bool IsEnableDebuggingRequestResponse { get; set; }
        [CanPrivateOverride(nameof(PrivateConfig.ElasticsearchHost))]
        public string[] Hosts { get; set; }
    }
}
