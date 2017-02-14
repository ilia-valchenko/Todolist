using Bootstrap;
using Bootstrap.Unity;
using System.Web.Http;
using Bootstrap.AutoMapper;
using Microsoft.Practices.Unity;
using System.Web.Http.Filters;
using System;

using RESTService.Infrastructure;
using Logger;

namespace RESTService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Bootstrapper.With.Unity().With.AutoMapper().Start();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Filters.Add(((IUnityContainer)Bootstrapper.Container).Resolve<IFilter>());
        }        
    }
}
