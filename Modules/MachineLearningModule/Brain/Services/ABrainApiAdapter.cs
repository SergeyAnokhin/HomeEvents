using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Config;
using MachineLearningModule.Brain.Model;
using MachineLearningModule.Events;

namespace MachineLearningModule.Brain.Services
{
    public abstract class ABrainApiAdapter : IBrainApiAdapter
    {
        protected readonly IApiService api;
        protected Config.Config Config { get; set; }

        protected ABrainApiAdapter(IApiService api, IAppConfigService configService)
        {
            this.api = api;
            Config = configService.GetModuleConfig<Config.Config>();
            api.Config(Config.ApiConfigs.First(c => c.ApiName == GetType().Name));
        }

        public ClassificationPrediction Predict(ClassificationInputData data)
        {
            return Predict(new[] {data}).FirstOrDefault();
        }

        public abstract bool IsActive();
        public abstract BrainInfo GetBrainInfo();
        public abstract IEnumerable<ClassificationPrediction> Predict(IEnumerable<ClassificationInputData> data);
        public abstract IEnumerable<ClassificationPrediction> Fit(IEnumerable<ClassificationInputData> data);
    }
}
