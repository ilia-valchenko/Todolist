using System.Web.Http;
using Microsoft.Practices.Unity;
using DAL.Interfaces.Repository.ModelRepository;
using DAL.Concrete;

namespace RESTService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            UnityContainer container = new UnityContainer();
            container.RegisterType<ITaskRepository, TaskRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new Infrastructure.UnityResolver(container);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
