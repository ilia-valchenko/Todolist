using Microsoft.Practices.Unity;
using DAL.Repositories.Interfaces;
using BLL.Services.Interfaces;
using BLL.Services;
using Infrastructure.Logger;
using RESTService.Handlers;
using System.Web.Http.Filters;
using BLL.Models;
using RESTService.ViewModels;
using AutoMapper;
using AutoMapper.Configuration;

namespace RESTService.Infrastructure
{
    public static class Bootstrapper
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            BLL.Infrastructure.Bootstrapper.RegisterTypes(container);

            container.RegisterType<ILogger, NLogLogger>(new ContainerControlledLifetimeManager());
            container.RegisterType<IFilter, HandleExceptionsAttribute>(new ContainerControlledLifetimeManager());

            container.RegisterType<ITaskService, TaskService>(new HierarchicalLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<ITaskRepository>(),
                                        new ResolvedParameter<IElasticRepository>(),
                                        "taskmanager"));
        }

        public static void RegisterMaps(MapperConfigurationExpression config)
        {
            BLL.Infrastructure.Bootstrapper.RegisterMaps(config);

            config.CreateMap<TaskModel, TaskViewModel>();
            config.CreateMap<CreateTaskViewModel, TaskModel>();
            config.CreateMap<EditTaskViewModel, TaskModel>();

            Mapper.Initialize(config);
        }
    }
}
