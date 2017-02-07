using Bootstrap;
using Bootstrap.Unity;
using System.Web.Http;
using RESTService.Infrastructure;
using Bootstrap.AutoMapper;
using Logger;

namespace RESTService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Bootstrapper.With.Unity().With.AutoMapper().Start();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Filters.Add(new HandleExceptionsAttribute(new NLogLogger()));
        }
    }
}
