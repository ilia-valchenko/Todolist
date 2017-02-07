using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
