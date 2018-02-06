using Prism.Modularity;
using Unity;

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
