using System;
using System.Linq;
using Common;
using Common.Config;
using MachineLearningModule.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MachineLearningTests
{
    [TestClass]
    public class HomeEventsServiceTests
    {
        private LogService log;

        [TestInitialize]
        public void init()
        {
            log = new LogService();
            log.Debug("Start test");
        }

        [TestMethod]
        public void BasicTest()
        {
            var config = new AppConfigService(log);
            var elastic = new ElasticSearchService(config, log);
            IHomeEventsService target = new HomeEventsService(config, elastic, log); 

            var result = target.GetEventsWindow(new DateTime(2018, 02, 03, 16, 04, 00));
            Assert.AreEqual(23, result.Count());
            Assert.IsNotNull(result.First()._id);
        }
    }
}
