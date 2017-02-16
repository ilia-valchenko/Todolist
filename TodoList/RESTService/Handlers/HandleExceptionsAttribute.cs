using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Infrastructure.Logger;
using Infrastructure.Logger.Models;

namespace RESTService.Handlers
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
            logger.LogError(new ErrorModel(context.Exception.Message, DateTime.Now, context.Exception.StackTrace));

            context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
               Content = new StringContent("Looks like something went wrong! We track these errors automatically, but if the problem persists feel free to contact us.In the meantime, try refreshing.")
            };
        }
    }
}