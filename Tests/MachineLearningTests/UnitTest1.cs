﻿using System.Collections.Generic;
using Common;
using MachineLearningModule.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MachineLearningTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AppConfigServiceTest()
        {
            var config = new AppConfigService();
            var obj = config.GetMyConfig();
            Assert.AreEqual("http://windowsserver:9200", obj["elasticsearch"])
        }

        [TestMethod]
        public void TestMethod1()
        {

            var expected = new List<string>()
            {
                "0#FrontWall(.) Foscam|Motion=1",
                "0#Hall(0) Door|Door=0",
                "0#Hall(0) Door|Door=1",
                "0#Corridor(1) Ceiling|Motion=1",
                "2#Mobile(.) Sony Xperia|Connection=0",
                "2#Corridor(1) Ceiling|Motion=1",
                "2#Hall(0) Ceiling|Motion=1",
                "2#Corridor(1) Ceiling|Motion=1",
                "3#Play(1) Door|Door=0",
                "3#Play(1) Door|Door=1",
                "3#Stairs(2) Pipe|Motion=1",
                "4#Corridor(1) Ceiling|Motion=1",
                "11#Stairs(2) Pipe|Motion=1",
                "12#Stairs(2) Pipe|Motion=1",
                "14#Stairs(2) Pipe|Motion=1",
                "14#Play(1) Door|Door=0",
                "14#Play(1) Door|Door=1",
                "14#Corridor(1) Ceiling|Motion=1",
                "15#Corridor(1) Ceiling|Motion=1",
                "15#Garage(0) Door|Door=0",
                "15#Hall(0) Ceiling|Motion=1",
                "16#Hall(0) Ceiling|Motion=1",
                "17#Corridor(1) Ceiling|Motion=1",
            };

            var config = new AppConfigService();
            var elastic = new ElasticSearchService(config);
            var target = new HomeEventsService(elastic);

            // target.GetSnapshot()
        }
    }
}
