using System.Linq;
using Common;
using MachineLearningModule.Brain.Services;
using Microsoft.Practices.ObjectBuilder2;
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

            container.RegisterType<IBrainApiAdapter, SkLearnBrainApiAdapter>("SkLearnBrainApiAdapter");
            //container.RegisterType<IBrainApiAdapter, TensorFlowBrainApiAdapter>("TensorFlowBrainApiAdapter");
            //container.RegisterType<IBrainApiAdapter, AccordNetBrainApiAdapter>("AccordNetBrainApiAdapter");

            container.Registrations.ForEach(r =>
                logger.Info($"[Register] <b>{r.RegisteredType.Name}</b> => <b>{r.MappedToType.Name}</b> (<u>{r.Name}</u>)"));
        }
    }
}
