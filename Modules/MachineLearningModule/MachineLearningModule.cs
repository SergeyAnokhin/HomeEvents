using System.Linq;
using Common;
using Prism.Modularity;
using Microsoft.Practices.Unity;

namespace MachineLearningModule
{
    public class MachineLearningModule: IModule
    {
        private readonly IUnityContainer container;

        public MachineLearningModule(IUnityContainer container)
        {
            this.container = container;
        }

        public void Initialize()
        {
            var logger = container.Resolve<ILogService>().Init(typeof(MachineLearningModule));
            var currentAssembly = typeof(MachineLearningModule).Assembly;
            container.RegisterTypes(
                AllClasses.FromAssemblies(currentAssembly).
                    Where(type => typeof(IService).IsAssignableFrom(type)),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.Transient);

            foreach (var registration in container.Registrations)
            {
                logger.Info($"UNITY: {registration.RegisteredType.Name} => {registration.MappedToType.Name} ({registration.Name})");
            }
        }
    }
}
