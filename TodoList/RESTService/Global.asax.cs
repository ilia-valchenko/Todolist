using System.Web.Http;
using RESTService.Handlers;
using Infrastructure.Logger;

namespace RESTService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Filters.Add(new HandleExceptionsAttribute(new NLogLogger()));
        }        
    }
}
