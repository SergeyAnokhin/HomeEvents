using Common;
using Common.Config;

namespace MachineLearningModule.Config
{
    public class Config
    {
        [OverrideIfExist(nameof(PrivateConfig.ElasticsearchHost))]
        public string[] ElasticsearchHost { get; set; }
        public int EventsWindowsSeconds { get; set; }
        public int EventsWindowsStep { get; set; }
    }
}
