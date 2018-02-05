using System;
using System.ComponentModel;
using System.Reflection;
using log4net;

namespace Common
{
    public interface ILogService
    {
        void Debug(string message);
        void Info(string message);
        void Error(string message);
        void Fatal(string message);
        ILogService Init(Type requestedType);
    }

    public class LogService : ILogService
    {
        private ILog logger = LogManager.GetLogger("undefined");

        public LogService()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public ILogService Init(Type requestedType)
        {
            logger = LogManager.GetLogger(requestedType);
            logger.Info($"[<b>{requestedType.Name}</b>] init");
            return this;
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Info(string message)
        {
            logger.Info(message);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Fatal(string message)
        {
            logger.Fatal(message);
        }
    }
}
