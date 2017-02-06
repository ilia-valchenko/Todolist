using System;

namespace Logger
{
    public interface ILogger
    {
        void LogError(Exception exception);
    }
}
