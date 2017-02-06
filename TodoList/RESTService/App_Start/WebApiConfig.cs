using System.Web.Http;
using Microsoft.Practices.Unity;
using Bootstrap.Unity;
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

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "SearchRoute",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
