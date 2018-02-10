using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Config;
using MachineLearningModule.Events;

namespace MachineLearningModule.Brain.Services
{
    public abstract class ABrainApiAdapter : IBrainApiAdapter
    {
        protected readonly IApiService api;
        protected Config.Config Config { get; set; }

        public ABrainApiAdapter(IApiService api, IAppConfigService configService)
        {
            this.api = api;
            Config = configService.GetModuleConfig<Config.Config>();
            api.Config(Config.ApiConfigs.First(c => c.ApiName == GetType().Name));
        }

        public abstract bool IsActive();
        public abstract BrainInfo GetBrainInfo();
        public abstract BrainPrediction Predict(IEnumerable<HomeEvent> events);
        public abstract BrainPrediction AddToModel(IEnumerable<HomeEvent> events, string className);
    }
}
