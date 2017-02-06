using System.Web.Http.Filters;
using Logger;

namespace RESTService.Infrastructure
{
    public class LogExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger logger;

        public LogExceptionFilterAttribute(ILogger logger)
        {
            this.logger = logger;
        }

        public override void OnException(HttpActionExecutedContext context)
        {
            logger.LogError(context.Exception);
        }
    }
}