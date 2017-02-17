﻿using System.Web.Http;
using Microsoft.Practices.Unity;
using RESTService.Infrastructure;

namespace RESTService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            IUnityContainer container = new UnityContainer();
            Bootstrapper.Initialize(container);
            config.DependencyResolver = new UnityResolver(container);
            RegisterRoutesHelper.MapRoutes(config);
        }
    }
}
