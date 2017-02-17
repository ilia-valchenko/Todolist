using Common.Logger.Models;

namespace Common.Logger
{
    public interface ILogger
    {
        void LogError(ErrorModel error);
    }
}
