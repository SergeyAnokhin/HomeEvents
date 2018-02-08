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
    public class EventsManagerIntegrationTests
    {
        private UnityContainer container;
        private MachineLearningModule.MachineLearningModule module;

        [TestInitialize]
        public void Init()
        {
            container = new UnityContainer();
            var common = new Common.CommonModule(container);
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
                @"Data\ElastiSearchServiceMock\ResponseEvents.json"
            });
            var apiMock = new ApiServiceMock
            {
                MockResponseData = new Dictionary<string, Stack<string>>
                {
                    {
                        "predict", new Stack<string>(new[]
                        {
                            @"Data\apiMockMockResponseData\predict.json",
                        })
                    },
                }
            };
            container.RegisterInstance<IElasticSearchService>(esMock);
            container.RegisterInstance<IApiService>(apiMock);

            var eventManager = container.Resolve<IEventsManager>();
            var events = eventManager.GetEventsForSelect(new DateTime(2018, 02, 03, 17, 04, 00));
            eventManager.BrainPredict(events.Select(e => e.Id).ToList());
            eventManager.SendToBrain(events.Select(e => e.Id).ToList(), "MasterCome");
        }
    }
}
