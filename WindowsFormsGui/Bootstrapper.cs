using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Modularity;

namespace WindowsFormsGui
{
    public class Bootstrapper : Prism.Unity.UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<DependencyObject>();
        }

        protected override void InitializeShell()
        {
            this.InitializeModules();
        }

        protected override void ConfigureModuleCatalog()
        {
            var catalog = (ModuleCatalog)ModuleCatalog;
            catalog.AddModule(typeof(MachineLearningModule.MachineLearningModule));
        }
    }
}
