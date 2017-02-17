using System.Web.Http;
using Microsoft.Practices.Unity;
using RESTService.Infrastructure;
using AutoMapper.Configuration;

namespace RESTService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            IUnityContainer container = new UnityContainer();
            MapperConfigurationExpression mapperConfig = new MapperConfigurationExpression();

            Bootstrapper.RegisterTypes(container);
            Bootstrapper.RegisterMaps(mapperConfig);
            
            config.DependencyResolver = new UnityResolver(container);
            RegisterRoutesHelper.MapRoutes(config);
        }
    }
}
