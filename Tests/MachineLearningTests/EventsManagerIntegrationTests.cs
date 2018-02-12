using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using CommonTests.Mocks;
using MachineLearningModule.Events;
using MachineLearningModule.Repositories;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MachineLearningTests
{
    [TestClass]
    public class EventsManagerIntegrationTests : AMockTest
    {
        private UnityContainer container;
        private MachineLearningModule.MachineLearningModule module;

        [TestInitialize]
        public void Init()
        {
            container = new UnityContainer();
            var common = new CommonModule(container);
            common.Initialize();

            module = new MachineLearningModule.MachineLearningModule(container);
            module.Initialize();
        }

        [TestMethod]
        public void BasicTest()
        {
            var esMock = new ElastiSearchServiceMock(new[]
            {
                @"Data\ElastiSearchServiceMock\ResponseEvents.json",
                @"Data\ElastiSearchServiceMock\ResponseEvents.json",
                @"Data\ElastiSearchServiceMock\ResponseEvents.json"
            });
            var apiMock = new ApiServiceMock
            {
                MockResponseData = new Dictionary<string, Stack<string>>
                {
                    {
                        "fit", new Stack<string>(new[]
                        {
                            @"Data\apiMockMockResponseData\simplePredictions.json",
                        })
                    },
                    {
                        "predict", new Stack<string>(new[]
                        {
                            @"Data\apiMockMockResponseData\simplePredictions.json",
                        })
                    },
                }
            };
            if (IsUseMock)
            {
                container.RegisterInstance<IElasticSearchService>(esMock);
                container.RegisterInstance<IApiService>(apiMock);
            }

            var eventManager = container.Resolve<IEventsManager>();

            // scenario
            var events = eventManager.GetEventsForSelect(new DateTime(2018, 02, 03, 17, 04, 00)).ToList();
            var selftest = eventManager.SendToBrain(events.Select(e => e.Id).ToList(), "MasterCome").ToList();
            var predictions = eventManager.BrainPredict(events.Select(e => e.Id).ToList()).ToList();

            Assert.IsNotNull(predictions);
            Assert.AreNotEqual(0, predictions.Count);
            Assert.IsTrue(predictions.All(p => p.Class == "MasterCome"), predictions.StringJoin(Environment.NewLine));

            Assert.IsNotNull(selftest);
            Assert.AreNotEqual(0, selftest.Count);
            Assert.IsTrue(selftest.All(t => Math.Abs(t.Probability - 1) < float.Epsilon), selftest.StringJoin());
        }

        [TestMethod]
        public void MultiSamplesAndOnePredictionTest()
        {
            Assert.Fail("TODO");
        }
    }
}
