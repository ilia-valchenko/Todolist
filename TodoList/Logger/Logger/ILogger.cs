using Infrastructure.Logger.Models;

namespace Infrastructure.Logger
{
    public interface ILogger
    {
        void LogError(ErrorModel error);
    }
}
