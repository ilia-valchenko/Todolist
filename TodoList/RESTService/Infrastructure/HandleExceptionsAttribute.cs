using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Logger;
using Newtonsoft.Json;

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
                Content = new StringContent(JsonConvert.SerializeObject(new { Message = context.Exception.Message }))
            };
        }
    }
}