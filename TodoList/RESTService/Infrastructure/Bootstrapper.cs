using Microsoft.Practices.Unity;
using BLL.Services.Interfaces;
using BLL.Services;
using BLL.Models;
using Infrastructure.Logger;
using RESTService.Handlers;
using RESTService.ViewModels;
using System.Web.Http.Filters;
using AutoMapper.Configuration;
using Common.Logger;

namespace RESTService.Infrastructure
{
    public static class Bootstrapper
    {
        public static void Initialize(IUnityContainer container)
        {
            BLL.Infrastructure.Bootstrapper.RegisterTypes(container);

            // remove it
            MapperConfigurationExpression config = new MapperConfigurationExpression();
            RegisterMaps(config);
            //

            container.RegisterType<Common.Mapper.IMapper, Common.Mapper.Mapper>(new ContainerControlledLifetimeManager(), new InjectionConstructor(config));

            container.RegisterType<ILogger, NLogLogger>(new ContainerControlledLifetimeManager());
            container.RegisterType<IFilter, HandleExceptionsAttribute>(new ContainerControlledLifetimeManager());
            container.RegisterType<ITaskService, TaskService>(new HierarchicalLifetimeManager());
        }

        private static void RegisterMaps(MapperConfigurationExpression config)
        {
            BLL.Infrastructure.Bootstrapper.RegisterMaps(config);

            config.CreateMap<TaskModel, TaskViewModel>();
            config.CreateMap<CreateTaskViewModel, TaskModel>();
            config.CreateMap<EditTaskViewModel, TaskModel>();
        }
    }
}
