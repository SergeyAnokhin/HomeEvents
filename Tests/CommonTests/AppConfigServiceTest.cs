using System.Linq;
using Common;
using Common.Config;
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
            Assert.AreEqual(2, obj.ElasticSearchService.Hosts.Length);
            CollectionAssert.Contains(obj.ElasticSearchService.Hosts, "http://windowsserver:9200");
            Assert.IsTrue(obj.ElasticSearchService.Hosts.Any(i => i.EndsWith(".com")));
            Assert.IsTrue(obj.ElasticSearchService.IsEnableDebuggingRequestResponse);
        }
    }
}
