using System.Web.Http;
using Microsoft.Practices.Unity;
using Bootstrap;
using RESTService.Infrastructure;

namespace RESTService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            IUnityContainer container = (IUnityContainer)Bootstrapper.Container;
            config.DependencyResolver = new UnityResolver(container);
            RegisterHelper.MapRoutes(config);
        }
    }
}
