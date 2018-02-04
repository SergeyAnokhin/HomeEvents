using Common;
using MachineLearningModule.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MachineLearningTests
{
    [TestClass]
    public class HomeEventsServiceTests
    {
        [TestMethod]
        public void BasicTest()
        {
            var config = new AppConfigService();
            var elastic = new ElasticSearchService(config);
            var target = new HomeEventsService(config, elastic);

            // target.GetEventsWindow();
        }
    }
}
