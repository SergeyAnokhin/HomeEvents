using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonTests
{
    [TestClass]
    public class AppConfigServiceTest
    {
        [TestMethod]
        public void BasicTest()
        {
            var log = new LogService();
            var config = new AppConfigService(log);
            var obj = config.GetModuleConfig<MachineLearningModule.Config.Config>();
            CollectionAssert.Contains(obj.ElasticsearchHost, "http://windowsserver:9200");
        }
    }
}
