using System;
using System.Linq;
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
            var esMock = new ElastiSearchServiceMock(new[] {@"Data\elasticResponseEvents.json"});
            container.RegisterInstance<IElasticSearchService>(esMock);

            var eventManager = container.Resolve<IEventsManager>();
            var events = eventManager.GetEventsForSelect(new DateTime(2018, 02, 03, 17, 04, 00));
            eventManager.SendToBrain(events.Select(e => e.Id).ToList(), "MasterCome");
        }
    }
}
