using System.Web.Http;
using Microsoft.Practices.Unity;
using DAL.Interfaces.Repository.ModelRepository;
using DAL.Concrete;
using BLL.Interfaces.Services.EntityService;
using BLL.Concrete;

namespace RESTService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            UnityContainer container = new UnityContainer();
            container.RegisterType<ITaskRepository, TaskRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ITaskService, TaskService>(new HierarchicalLifetimeManager());
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
