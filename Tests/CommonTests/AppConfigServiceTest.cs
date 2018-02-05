﻿using Common;
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
            Assert.AreEqual("http://windowsserver:9200", obj.ElasticsearchHost);
        }
    }
}
