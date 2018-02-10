using System;
using Common;
using CommonTests.Mocks;
using MachineLearningModule.Brain.Services;
using MachineLearningModule.Config;
using MachineLearningModule.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MachineLearningTests.BrainApiAdapters
{
    [TestClass]
    public class SkLearnBrainApiAdapterTest
    {
        private LogService log;
        private AppConfigServiceMock appConfigMock;
        private ApiServiceMock apiMock;
        private SkLearnBrainApiAdapter target;
        private DateTime end;

        [TestInitialize]
        public void Init()
        {
            log = new LogService();
            log.Debug("Start test");
            appConfigMock = new AppConfigServiceMock();
            apiMock = new ApiServiceMock();
            appConfigMock.Configs.Add(new Config
            {
                SkLearnBrainApiAdapterConfig = new SkLearnBrainApiAdapterConfig
                {
                    ImageStepSeconds = 30,
                    TotalImageSteps = 3,
                    EventsOrder = new[] { "[M]Event=1", "[M]Event=2", "[M]Event=3" }
                }
            });
            target = new SkLearnBrainApiAdapter(apiMock, appConfigMock);

            end = new DateTime(2018, 01, 01, 00, 00, 00);
        }

        [TestMethod]
        public void OneEventTest()
        {
            var result = target.ConvertToModelImage(new []
            {
                CreateEvent(0, 1),
            });

            Assert.AreEqual(9, result.Image.Length, result.Image.StringJoin());
            CollectionAssert.AreEqual(new []
            {
                1, 0, 0,
                0, 0, 0,
                0, 0, 0
            }, result.Image, result.ToString());
        }

        [TestMethod]
        public void TwoEventTest()
        {
            var result = target.ConvertToModelImage(new[]
            {
                CreateEvent(0, 1), CreateEvent(0, 3),
            });
            CollectionAssert.AreEqual(new[]
            {
                1, 0, 1,
                0, 0, 0,
                0, 0, 0
            }, result.Image, result.ToString());
        }

        [TestMethod]
        public void TwoSameEventTest()
        {
            var result = target.ConvertToModelImage(new[]
            {
                CreateEvent(0, 1), CreateEvent(0, 1, 15),
            });
            CollectionAssert.AreEqual(new[]
            {
                2, 0, 0,
                0, 0, 0,
                0, 0, 0
            }, result.Image, result.ToString());
        }

        [TestMethod]
        public void DoublonTest()
        {
            var result = target.ConvertToModelImage(new[]
            {
                CreateEvent(0, 1), CreateEvent(0, 1),
            });
            CollectionAssert.AreEqual(new[]
            {
                1, 0, 0,
                0, 0, 0,
                0, 0, 0
            }, result.Image, result.ToString());
        }

        [TestMethod]
        public void DiffInStepTest()
        {
            var result = target.ConvertToModelImage(new[]
            {
                CreateEvent(0, 1), CreateEvent(1, 3, 15),
            });
            CollectionAssert.AreEqual(new[]
            {
                1, 0, 0,
                0, 0, 1,
                0, 0, 0
            }, result.Image, result.ToString());
        }

        private HomeEvent CreateEvent(int step, int num, int addInSec = 0)
        {
            return new HomeEvent
            {
                DateTime = end.AddSeconds(-step * 30 - addInSec),
                SensorType = "M",
                Sensor = "Event",
                Status = num.ToString()
            };
        }
    }


    /*
"[Motion]FrontWall(.) Foscam=On",
"[Door]Hall(0) Door=Off",
"[Door]Hall(0) Door=On",
"[Motion]Corridor(1) Ceiling=On",
"[Connection]Mobile(.) Sony Xperia=Off",
"[Motion]Corridor(1) Ceiling=On",
"[Motion]Hall(0) Ceiling=On",
"[Motion]Corridor(1) Ceiling=On",
"[Door]Play(1) Door=Off",
"[Door]Play(1) Door=On",
"[Motion]Stairs(2) Pipe=On",
"[Motion]Corridor(1) Ceiling=On",
"[Motion]Stairs(2) Pipe=On",
"[Motion]Stairs(2) Pipe=On",
"[Motion]Stairs(2) Pipe=On",
"[Door]Play(1) Door=Off",
"[Door]Play(1) Door=On",
"[Motion]Corridor(1) Ceiling=On",
"[Motion]Corridor(1) Ceiling=On",
"[Door]Garage(0) Door=Closed",
"[Motion]Hall(0) Ceiling=On",
"[Motion]Hall(0) Ceiling=On",
"[Motion]Corridor(1) Ceiling=On"
     */
}
