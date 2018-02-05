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
    }

    public class LogService : ILogService
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public LogService()
        {
            log4net.Config.XmlConfigurator.Configure();
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
