using System;
using log4net;

namespace Common
{
    public interface ILogService : IService
    {
        void Debug(string message);
        void Info(string message);
        void Warn(string message);
        void Error(string message);
        void Fatal(string message);
        ILogService Init(Type requestedType);
        ILogService Init(Type requestedType, string name);
    }

    public class LogService : ILogService
    {
        private ILog logger = LogManager.GetLogger(typeof(LogService));

        static LogService()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public ILogService Init(Type requestedType, string name)
        {
            logger = LogManager.GetLogger(name);
            logger.Info($"<b>{requestedType.Name}</b> ({name}) init");
            return this;
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

        public void Warn(string message)
        {
            logger.Warn(message);
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
