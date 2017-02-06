using Bootstrap;
using Bootstrap.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using RESTService.Infrastructure;
using Bootstrap.AutoMapper;

namespace RESTService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Bootstrapper.With.Unity().With.AutoMapper().Start();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        //protected void Application_Error()
        //{
        //    HttpContext context = HttpContext.Current;

        //    if(context != null)
        //    {
        //        RequestContext requestContext = 
        //    }
        //}
    }
}
