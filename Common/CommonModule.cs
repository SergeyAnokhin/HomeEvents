using Common.Config;
using Microsoft.Practices.Unity;
using Prism.Modularity;

namespace Common
{
    public class CommonModule : IModule
    {
        private readonly IUnityContainer container;

        public CommonModule(IUnityContainer container)
        {
            this.container = container;
        }

        public void Initialize()
        {
            container.RegisterType<ILogService, LogService>();
            container.RegisterType<IAppConfigService, AppConfigService>();
        }
    }
}
