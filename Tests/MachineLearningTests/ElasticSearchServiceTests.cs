using System.Collections.Generic;
using Common;
using MachineLearningModule.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MachineLearningTests
{
    [TestClass]
    public class ElasticSearchServiceTests
    {
        [TestMethod]
        public void TestMethod1()
        {


            var config = new AppConfigService();
            var elastic = new ElasticSearchService(config);

            // target.GetSnapshot()
        }
    }
}
