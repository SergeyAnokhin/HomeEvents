using System.Linq;
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
            Assert.AreEqual(2, obj.ElasticsearchHost.Length);
            CollectionAssert.Contains(obj.ElasticsearchHost, "http://windowsserver:9200");
            Assert.IsTrue(obj.ElasticsearchHost.Any(i => i.EndsWith(".com")));
        }
    }
}
