using NLog;
using Common.Logger.Models;

namespace Infrastructure.Logger
{
    public sealed class NLogLogger :  Common.Logger.ILogger
    {
        private readonly NLog.Logger logger;

        public NLogLogger()
        {
            logger = LogManager.GetCurrentClassLogger();
        }

        public void LogError(ErrorModel error)
        {
            logger.Error(error);
        }
    }
}
