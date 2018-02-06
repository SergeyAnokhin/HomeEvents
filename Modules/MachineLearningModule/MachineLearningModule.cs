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
        }
    }
}
