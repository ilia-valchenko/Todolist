using NLog;
using Infrastructure.Logger.Models;

namespace Infrastructure.Logger
{
    public sealed class NLogLogger :  ILogger
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
