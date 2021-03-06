﻿using System.Collections.Generic;
using Common;
using Common.Config;

namespace MachineLearningModule.Config
{
    public class Config
    {
        public int EventsWindowsSeconds { get; set; }
        public ElasticSearchServiceConfig ElasticSearchService { get; set; }
        public SkLearnBrainApiAdapterConfig SkLearnBrainApiAdapterConfig { get; set; }
        public List<ApiServiceConfig> ApiConfigs { get; set; }
    }

    public class SkLearnBrainApiAdapterConfig
    {
        public string[] EventsOrder { get; set; }
        public int ImageStepSeconds { get; set; }
        public int TotalImageSteps { get; set; }
    }

    public class ElasticSearchServiceConfig
    {
        public bool IsEnableDebuggingRequestResponse { get; set; }
        [CanPrivateOverride(nameof(PrivateConfig.ElasticsearchHost))]
        public string[] Hosts { get; set; }
    }
}
