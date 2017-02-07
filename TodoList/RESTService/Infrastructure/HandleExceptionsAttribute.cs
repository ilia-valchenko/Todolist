using System.Web.Http.Filters;
using Logger;

namespace RESTService.Infrastructure
{
    public class HandleExceptionsAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger logger;

        public HandleExceptionsAttribute(ILogger logger)
        {
            this.logger = logger;
        }

        public override void OnException(HttpActionExecutedContext context)
        {
            logger.LogError(context.Exception);
        }
    }
}