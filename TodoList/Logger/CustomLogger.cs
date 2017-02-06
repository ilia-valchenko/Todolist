using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog.Common;

namespace Logger
{
    public class CustomLogger : ILogger
    {
        //private const Logger logger = 
        public void LogError(Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}
