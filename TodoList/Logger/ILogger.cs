using System;

namespace Logger
{
    public interface ILogger
    {
        void LogError(ErrorModel error);
    }
}
