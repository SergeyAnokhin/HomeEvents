using System.Linq;
using Microsoft.Practices.ObjectBuilder2;
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
            var currentAssembly = typeof(CommonModule).Assembly;
            container.RegisterTypes(
                AllClasses.FromAssemblies(currentAssembly).
                    Where(type => typeof(IService).IsAssignableFrom(type)),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.Transient);
            var logger = container.Resolve<ILogService>().Init(typeof(CommonModule));

            container.Registrations.ForEach(r => 
                logger.Info($"[Register] <b>{r.RegisteredType.Name}</b> => <b>{r.MappedToType.Name}</b> (<u>{r.Name}</u>)"));
        }
    }
}
