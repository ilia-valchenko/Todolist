using System;
using NLog;

namespace Logger
{
    public class NLogLogger :  ILogger
    {
        private readonly NLog.Logger logger;

        public NLogLogger()
        {
            logger = LogManager.GetCurrentClassLogger();
        }

        public void LogError(Exception exception)
        {
            logger.Error(exception);
        }
    }
}
