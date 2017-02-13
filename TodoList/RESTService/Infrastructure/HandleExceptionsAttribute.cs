using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Logger;

namespace RESTService.Infrastructure
{
    public sealed class HandleExceptionsAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger logger;

        public HandleExceptionsAttribute(ILogger logger)
        {
            this.logger = logger;
        }

        public override void OnException(HttpActionExecutedContext context)
        {
            logger.LogError(context.Exception);

            context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent($"Oops! Sorry! Something went wrong. Error message: {context.Exception.Message}. See the link for more information: {context.Exception.HelpLink}")
            };
        }
    }
}